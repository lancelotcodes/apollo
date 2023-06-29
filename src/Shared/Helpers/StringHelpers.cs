﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Shared.Helpers
{
    public class SemiNumericComparer : IComparer<string>
    {
        /// <summary>
        /// Method to determine if a string is a number
        /// </summary>
        /// <param name="value">String to test</param>
        /// <returns>True if numeric</returns>
        public static bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }

        /// <inheritdoc />
        public int Compare(string s1, string s2)
        {
            const int S1GreaterThanS2 = 1;
            const int S2GreaterThanS1 = -1;

            var IsNumeric1 = IsNumeric(s1);
            var IsNumeric2 = IsNumeric(s2);

            if (IsNumeric1 && IsNumeric2)
            {
                var i1 = Convert.ToInt32(s1);
                var i2 = Convert.ToInt32(s2);

                if (i1 > i2)
                {
                    return S1GreaterThanS2;
                }

                if (i1 < i2)
                {
                    return S2GreaterThanS1;
                }

                return 0;
            }

            if (IsNumeric1)
            {
                //return S2GreaterThanS1;

                if (s1.ToLower().Equals("penthouse"))
                {
                    return S2GreaterThanS1;
                }
                if (s1.ToLower().Equals("upper ph"))
                {
                    return S2GreaterThanS1;
                }
                if (s1.ToLower().Equals("lower ph"))
                {
                    return S2GreaterThanS1;
                }
                if (s1.Equals("Vacant"))
                {
                    return S2GreaterThanS1;
                }
                if (s1.Equals("MGMT"))
                {
                    return S2GreaterThanS1;
                }
                if (s1.Equals("Not Verified"))
                {
                    return S1GreaterThanS2;
                }

                return S1GreaterThanS2;
            }

            if (IsNumeric2)
            {
                //return S1GreaterThanS2;

                if (s1.ToLower().Equals("penthouse"))
                {
                    return S2GreaterThanS1;
                }
                if (s1.ToLower().Equals("upper ph"))
                {
                    return S2GreaterThanS1;
                }
                if (s1.ToLower().Equals("lower ph"))
                {
                    return S2GreaterThanS1;
                }
                if (s2.Equals("Vacant"))
                {
                    return S1GreaterThanS2;
                }
                if (s2.Equals("MGMT"))
                {
                    return S1GreaterThanS2;
                }
                if (s1.Equals("Not Verified"))
                {
                    return S2GreaterThanS1;
                }

                return S2GreaterThanS1;
            }

            return string.Compare(s1, s2, true, CultureInfo.InvariantCulture);
        }
    }
}
