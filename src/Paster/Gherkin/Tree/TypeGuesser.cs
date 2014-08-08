using System;
using System.Text.RegularExpressions;

namespace xBehave.Paster.Gherkin
{
    internal static class TypeGuesser
    {
        private static readonly string DoubleType = "double";
        private static readonly string IntType = "int";
        private static readonly string StringType = "string";

        private static readonly Regex IsItADouble = new Regex(@"^[\d\.]+$", RegexOptions.Compiled);
        private static readonly Regex IsItAnInt = new Regex(@"^[\d]+$", RegexOptions.Compiled);

        public static string Guess(string value)
        {
            if (IsItAnInt.IsMatch(value))
                return IntType;
            if (IsItADouble.IsMatch(value))
                return DoubleType;
            return StringType;
        }
    }
}