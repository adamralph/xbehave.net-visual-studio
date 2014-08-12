using System.Text.RegularExpressions;

namespace xBehave.Paster.Gherkin
{
    internal static class TypeGuesser
    {
        private static readonly Regex IsItADouble = new Regex(@"^[\d\.]+$", RegexOptions.Compiled);
        private static readonly Regex IsItAnInt = new Regex(@"^[\d]+$", RegexOptions.Compiled);

        public static ValueTypes Guess(string value)
        {
            if (IsItAnInt.IsMatch(value))
                return ValueTypes.Int;
            if (IsItADouble.IsMatch(value))
                return ValueTypes.Double;
            return ValueTypes.String;
        }
    }
}