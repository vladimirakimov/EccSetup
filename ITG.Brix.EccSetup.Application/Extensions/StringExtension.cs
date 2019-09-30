using System;
using System.Linq;

namespace ITG.Brix.EccSetup.Application.Extensions
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static bool StartsWithLetter(this string str)
        {
            var result = false;
            if (!string.IsNullOrWhiteSpace(str))
            {
                result = char.IsLetter(str[0]);
            }
            return result;
        }

        public static bool StartsWithCapitalLetter(this string str)
        {
            var result = false;
            if (!string.IsNullOrWhiteSpace(str))
            {
                result = char.IsLetter(str[0]) && str[0].ToString() == str[0].ToString().ToUpperInvariant();
            }
            return result;
        }

        public static bool AtLeastOneSpecialCharacter(this string str)
        {
            var result = false;
            if (!string.IsNullOrWhiteSpace(str))
            {
                result = str.Count(p => !char.IsLetterOrDigit(p)) > 0;
            }
            return result;
        }

        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }

        public static bool NotStartsAndNotEndsWithWhiteSpace(this string str)
        {
            var result = false;
            if (!string.IsNullOrWhiteSpace(str))
            {
                result = !str.StartsWith(" ") && !str.EndsWith(" ");
            }
            return result;
        }

    }
}
