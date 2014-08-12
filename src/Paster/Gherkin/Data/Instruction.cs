using System;
using System.Linq;
using System.Text;

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

        public void Append(StringBuilder sb, Substitution[] substitutions)
        {
            var data = substitutions.Aggregate(_textLine, (current, pair) => current.Replace(pair.PlaceHolder, pair.NewValue));

            sb.AppendFormat(@"""{0}"".{1}(() => {{}});{2}", data, _instructionType, Environment.NewLine);
        }
    }
}