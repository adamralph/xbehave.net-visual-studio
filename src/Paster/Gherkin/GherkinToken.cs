namespace xBehave.Paster.Gherkin
{
    internal class GherkinToken
    {
        internal LineType Type { get; private set; }
        internal string RawLine { get; private set; }

        public GherkinToken(LineType type, string rawLine)
        {
            Type = type;
            RawLine = rawLine;
        }
    }
}