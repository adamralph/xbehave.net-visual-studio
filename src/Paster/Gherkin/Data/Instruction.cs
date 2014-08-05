using System;
using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    internal class Instruction : SyntaxNode
    {
        private readonly string _textLine;
        private readonly LineType _instructionType;

        public Instruction(string textLine, LineType instructionType)
        {
            _textLine = textLine.EscapeDoubleQuotes();
            _instructionType = instructionType;
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendFormat(@"""{0}"".{1}(() => {{}});{2}", _textLine, _instructionType, Environment.NewLine);
        }
    }
}