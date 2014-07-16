using System;
using SiliconSharkLtd.Paster;

namespace Paster.Specs.Fakes
{
    public class TestEnvironment : IEnvironment
    {
        public string[] LinesWritten { get; set; }

        public void Paste(string codeLines)
        {
            LinesWritten = codeLines.Split(new[] {Environment.NewLine},
                                           StringSplitOptions.RemoveEmptyEntries);
        }
    }
}