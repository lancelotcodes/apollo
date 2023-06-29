namespace Shared.Extensions
{
    public static class StringExtension
    {
        public static bool HasValue(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool LowerEquals(this string str, string value)
        {
            return str.ToLower().Equals(value.ToLower());
        }

        public static bool LowerContains(this string str, string value)
        {
            return str.ToLower().Contains(value.ToLower());
        }
    }
}
