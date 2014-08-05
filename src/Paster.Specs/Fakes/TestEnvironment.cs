using System;
using xBehave.Paster.System;

namespace Paster.Specs.Fakes
{
    public class TestEnvironment : DevelopmentEnvironment
    {
        public string TextWritten { get; set; }
        public string[] LinesWritten { get; set; }

        public TestEnvironment()
        {
            TextWritten = string.Empty;
            LinesWritten = new string[0];
        }

        public void Paste(string codeLines)
        {
            TextWritten = codeLines;
            LinesWritten = codeLines.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}