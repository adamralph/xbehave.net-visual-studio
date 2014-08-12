using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    internal class Scenario : SyntaxGroupingNode
    {
        private readonly string _methodName;
        private readonly List<SyntaxNode> _lines = new List<SyntaxNode>();

        private PlaceHolderCollection _placeholders = PlaceHolderCollection.Empty;

        public Scenario(string rawLine)
        {
            _methodName = rawLine.RemoveQuotes()
                                 .ToMethodCase();
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendLine("[Scenario]");
            if (_placeholders.Any())
            {
                var exampleAttributes = _placeholders.CreateExampleAttributes();
                sb.AppendLines(exampleAttributes);

                var paramList = _placeholders.CreateParameters();
                sb.AppendFormat(@"public void {0}({2}){1}{{{1}", _methodName, Environment.NewLine, String.Join(", ", paramList));
            }
            else
                sb.AppendFormat(@"public void {0}(){1}{{{1}", _methodName, Environment.NewLine);

            var substitutions = _placeholders.CreateSubstitutions();
            foreach (var appender in _lines)
                appender.Append(sb, substitutions);

            sb.AppendLine("}");
        }

        public void AddNode(Instruction node)
        {
            _lines.Add(node);
        }

        public void AddExample(string[] exampleNames)
        {
            _placeholders = new PlaceHolderCollection(exampleNames);
        }

        public void AddData(string[] variableValues)
        {
            var examples = variableValues.Select((v, index) => new ExampleValue(index, v))
                                         .ToArray();
            _placeholders.AddValues(examples);
        }
    }
}