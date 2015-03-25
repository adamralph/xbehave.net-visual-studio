using System;
using System.Linq;
using System.Text;

namespace xBehave.Paster.Gherkin
{
    internal class Instruction : SyntaxNode
    {
        public Instruction(string textLine)
        {
            _textLine = textLine.EscapeDoubleQuotes();
        }

        private readonly string _textLine;

        public void Append(StringBuilder sb, Substitution[] substitutions)
        {
            var data = substitutions.Aggregate(_textLine,
                                               (current, pair) => current.Replace(pair.PlaceHolder, pair.NewValue));

            sb.AppendFormat(@"""{0}"".f(() => {{}});{1}", data, Environment.NewLine);
        }
    }
}