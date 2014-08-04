using System;
using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    internal class Instruction : IStringAppender
    {
        private readonly string _textLine;
        private readonly Gherkin _gherkinType;

        public Instruction(string textLine, Gherkin gherkinType)
        {
            _textLine = textLine.Replace("\"", "\\\"");
            _gherkinType = gherkinType;
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendFormat(@"""{0}"".{1}(() => {{}});{2}", _textLine, _gherkinType, Environment.NewLine);
        }
    }
}