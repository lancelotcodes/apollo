using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace apollo.Application.Common.Extensions
{
    public static class Helpers
    {
        public static string ToURLSlug(string value)
        {
            //First to lower case 
            value = value.ToLowerInvariant();

            ////Remove all accents
            //var bytes = Encoding.GetEncoding(1251).GetBytes(value);

            //value = Encoding.ASCII.GetString(bytes);

            //Replace spaces 
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars 
            value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

            //Trim dashes from end 
            value = value.Trim('-', '_');

            //Replace double occurences of - or \_ 
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            //var randomGuid = Guid.NewGuid().ToString().ToLower();

            //value = value + "-" + randomGuid.Substring(randomGuid.Length - 6);

            return value;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, bool isAscending = true)
        {
            if (String.IsNullOrEmpty(columnName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        public static string GeneratePassword(
            bool includeLowercase, 
            bool includeUppercase, 
            bool includeNumeric, 
            bool includeSpecial, 
            bool includeSpaces, 
            int lengthOfPassword)
        {
            const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
            const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMERIC_CHARACTERS = "0123456789";
            const string SPECIAL_CHARACTERS = @"!#$%&*@\";
            const string SPACE_CHARACTER = " ";
            const int PASSWORD_LENGTH_MIN = 8;
            const int PASSWORD_LENGTH_MAX = 128;

            if (lengthOfPassword < PASSWORD_LENGTH_MIN || lengthOfPassword > PASSWORD_LENGTH_MAX)
            {
                return "Password length must be between 8 and 128.";
            }

            string characterSet = "";

            if (includeLowercase)
            {
                characterSet += LOWERCASE_CHARACTERS;
            }

            if (includeUppercase)
            {
                characterSet += UPPERCASE_CHARACTERS;
            }

            if (includeNumeric)
            {
                characterSet += NUMERIC_CHARACTERS;
            }

            if (includeSpecial)
            {
                characterSet += SPECIAL_CHARACTERS;
            }

            if (includeSpaces)
            {
                characterSet += SPACE_CHARACTER;
            }

            char[] password = new char[lengthOfPassword];
            int characterSetLength = characterSet.Length;

            System.Random random = new System.Random();

            for (int characterPosition = 0; characterPosition < lengthOfPassword; characterPosition++)
            {
                password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

                bool moreThanTwoIdenticalInARow =
                    characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                    && password[characterPosition] == password[characterPosition - 1]
                    && password[characterPosition - 1] == password[characterPosition - 2];

                if (moreThanTwoIdenticalInARow)
                {
                    characterPosition--;
                }
            }

            return string.Join(null, password);
        }

        public static bool PasswordIsValid(bool includeLowercase, bool includeUppercase, bool includeNumeric, bool includeSpecial, bool includeSpaces, string password)
        {
            const string REGEX_LOWERCASE = @"[a-z]";
            const string REGEX_UPPERCASE = @"[A-Z]";
            const string REGEX_NUMERIC = @"[\d]";
            const string REGEX_SPECIAL = @"([!#$%&*@\\])+";
            const string REGEX_SPACE = @"([ ])+";

            bool lowerCaseIsValid = !includeLowercase || (includeLowercase && Regex.IsMatch(password, REGEX_LOWERCASE));
            bool upperCaseIsValid = !includeUppercase || (includeUppercase && Regex.IsMatch(password, REGEX_UPPERCASE));
            bool numericIsValid = !includeNumeric || (includeNumeric && Regex.IsMatch(password, REGEX_NUMERIC));
            bool symbolsAreValid = !includeSpecial || (includeSpecial && Regex.IsMatch(password, REGEX_SPECIAL));
            bool spacesAreValid = !includeSpaces || (includeSpaces && Regex.IsMatch(password, REGEX_SPACE));

            return lowerCaseIsValid && upperCaseIsValid && numericIsValid && symbolsAreValid && spacesAreValid;
        }

        public static string[] ColourValues = new string[] {
                "#031d44"
                ,"#39275c"
                ,"#6c2c6a"
                ,"#9e306c"
                ,"#ca3c62"
                ,"#eb564f"
                ,"#fd7b34"
                ,"#ffa600",
                "#488f31",
                "#f69f62",
                "#df5851"

        };

        public static double GetDistance(string Source, string Destination)
        {
            double sourceLat = Convert.ToDouble(Source.Split(',')[0]);
            double sourceLon = Convert.ToDouble(Source.Split(',')[1]);

            double destinationLat = Convert.ToDouble(Destination.Split(',')[0]);
            double destinationLon = Convert.ToDouble(Destination.Split(',')[1]);

            double distance;
            try
            {
                GeoCoordinate source = new GeoCoordinate(sourceLat, sourceLon);
                GeoCoordinate destination = new GeoCoordinate(destinationLat, destinationLon);
                distance = source.GetDistanceTo(destination);
            }
            catch
            {
                distance = 100000;
            }

            return distance;
        }

        public static async Task<double> GetDistanceAsync(string Source, string Destination)
        {
            double sourceLat = Convert.ToDouble(Source.Split(',')[0]);
            double sourceLon = Convert.ToDouble(Source.Split(',')[1]);

            double destinationLat = Convert.ToDouble(Destination.Split(',')[0]);
            double destinationLon = Convert.ToDouble(Destination.Split(',')[1]);

            double distance;
            try
            {
                GeoCoordinate source = new GeoCoordinate(sourceLat, sourceLon);
                GeoCoordinate destination = new GeoCoordinate(destinationLat, destinationLon);
                distance = await Task.FromResult(source.GetDistanceTo(destination));
            }
            catch
            {
                distance = 100000;
            }

            return distance;
        }
    }
}
