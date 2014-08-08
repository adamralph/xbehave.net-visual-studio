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

        public void Append(StringBuilder sb, string[] substitutions)
        {
            var subPairs =
                substitutions.Select(
                                     (value, index) =>
                                     new Tuple<string, string>(String.Format("<{0}>", value), String.Format("{{{0}}}", index)));
            var data = subPairs.Aggregate(_textLine, (current, pair) => current.Replace(pair.Item1, pair.Item2));

            sb.AppendFormat(@"""{0}"".{1}(() => {{}});{2}", data, _instructionType, Environment.NewLine);
        }
    }
}