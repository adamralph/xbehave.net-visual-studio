using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xBehave.Paster.Gherkin
{
    internal class Scenario : SyntaxGroupingNode
    {
        private readonly string _methodName;
        private readonly List<SyntaxNode> _lines = new List<SyntaxNode>();
        private string[] _parameterNames = {};
        private readonly List<ExampleValue[]> _examples = new List<ExampleValue[]>();

        public Scenario(string rawLine)
        {
            _methodName = rawLine.RemoveQuotes()
                                 .ToMethodCase();
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendLine("[Scenario]");
            if (_parameterNames.Any() && _examples.Any())
            {
                foreach (var exampleValuese in _examples)
                {
                    string valueString = String.Join(",", exampleValuese.Select(ev => ev.Value));
                    sb.AppendLine(string.Format("[Example({0})]", valueString));
                }
                var paramList = _parameterNames.Select((name, index) => _examples[0][index].ValueType + " " + name);
                sb.AppendFormat(@"public void {0}({2}){1}{{{1}", _methodName, Environment.NewLine, String.Join(", ", paramList));
            }
            else
                sb.AppendFormat(@"public void {0}(){1}{{{1}", _methodName, Environment.NewLine);

            foreach (var appender in _lines)
                appender.Append(sb, _parameterNames);

            sb.AppendLine("}");
        }

        public void AddNode(Instruction node)
        {
            _lines.Add(node);
        }

        public void AddExample(string[] exampleNames)
        {
            _parameterNames = exampleNames;
        }

        public void AddData(string[] variableValues)
        {
            var examples = variableValues.Select((v, index) => new ExampleValue(index, v))
                                         .ToArray();

            _examples.Add(examples);
        }
    }
}