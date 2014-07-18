using System;
using System.Collections.Generic;
using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    internal class GherkinTree
    {
        private readonly IDictionary<Identifier, Func<string, dynamic>> _lineGenerators;
        private readonly Func<string, Identifier> _lineIdentifier;
        private IList<IStringAppender> _parent;
        private readonly IList<IStringAppender> _collection = new List<IStringAppender>();

        public GherkinTree(IDictionary<Identifier, Func<string, dynamic>> lineGenerators,
                           Func<string, Identifier> lineIdentifier)
        {
            _lineGenerators = lineGenerators;
            _lineIdentifier = lineIdentifier;
            _parent = _collection;
        }

        public void AddLines(IEnumerable<string> gherkinLines)
        {
            foreach (var textLine in gherkinLines)
            {
                var line = Build(textLine);
                if (line is ScenarioLine)
                    AddScenario(line);
                else
                    AddLine(line);
            }
        }

        private dynamic Build(string textLine)
        {
            var gherkinType = _lineIdentifier(textLine);
            return _lineGenerators[gherkinType](textLine);
        }

        private void AddLine(IStringAppender line)
        {
            _parent.Add(line);
        }

        private void AddScenario(ScenarioLine line)
        {
            _collection.Add(line);
            _parent = line;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var appender in _collection)
                appender.Append(sb);
            return sb.ToString();
        }
    }
}