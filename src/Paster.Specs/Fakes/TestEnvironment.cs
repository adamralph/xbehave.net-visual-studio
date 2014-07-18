using System;
using xBehave.Paster.System;

namespace Paster.Specs.Fakes
{
    public class TestEnvironment : IEnvironment
    {
        public string TextWritten { get; set; }
        public string[] LinesWritten { get; set; }

        public void Paste(string codeLines)
        {
            TextWritten = codeLines;
            LinesWritten = codeLines.Split(new[] {Environment.NewLine},
                                           StringSplitOptions.RemoveEmptyEntries);
        }
    }
}