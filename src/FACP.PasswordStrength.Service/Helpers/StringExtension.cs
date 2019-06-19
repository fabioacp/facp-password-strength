using System;
namespace FACP.PasswordStrength.Service.Helpers
{
    public static class StringExtension
    {
        public static bool IsNull(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static string Prefix(this string input, int length)
        {
            if (input.IsNull())
                return string.Empty;

            return input.Substring(0, length);
        }

        public static string Suffix(this string input, int start)
        {
            if (input.IsNull())
                return string.Empty;

            return input.Substring(start, input.Length - start);
        }

        public static string NumberOfTimesAppearedInDataBreaches(this string input)
        {
            if (input.IsNull())
                return null;

            var startIndex = input.IndexOf(":", StringComparison.Ordinal) + 1;

            if (startIndex == 0)
                return null;

            return input.Substring(startIndex, input.Length - startIndex);
        }
    }
}
