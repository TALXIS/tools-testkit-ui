using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALXIS.TestKit.Selectors.Extentions
{
    public static class StringExtensions
    {
        public static bool IsValueEqual(this string actual, string expected)
        {
            if (actual == null || expected == null)
                return string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase);

            bool isActualNumeric = TryParseNumeric(actual, out decimal actualNumber);
            bool isExpectedNumeric = TryParseNumeric(expected, out decimal expectedNumber);

            if (isActualNumeric && isExpectedNumeric)
            {
                return actualNumber == expectedNumber;
            }
            else
            {
                return string.Equals(actual.Trim(), expected.Trim(), StringComparison.OrdinalIgnoreCase);
            }
        }

        public static bool IsValueEqual(this string actual, string expected, string expectedType)
        {
            if (string.Equals(expectedType, "text", StringComparison.OrdinalIgnoreCase))
            {
                return string.Equals(actual.Trim(), expected.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            else if (string.Equals(expectedType, "numeric", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(expectedType, "currency", StringComparison.OrdinalIgnoreCase))
            {
                if (TryParseNumeric(actual, out decimal actualNumber) &&
                    TryParseNumeric(expected, out decimal expectedNumber))
                {
                    return actualNumber == expectedNumber;
                }
                return false;
            }
            else
            {
                return string.Equals(actual.Trim(), expected.Trim(), StringComparison.OrdinalIgnoreCase);
            }
        }

        private static bool TryParseNumeric(string s, out decimal number)
        {
            var cleaned = new string(s.Where(c => char.IsDigit(c) || c == '.' || c == ',' || c == '-' || c == '+').ToArray());

            return decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out number);
        }
    }
}
