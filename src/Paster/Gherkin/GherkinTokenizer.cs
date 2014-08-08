using System;
using System.Collections.Generic;

namespace xBehave.Paster.Gherkin
{
    internal static class GherkinTokenizer
    {
        internal static IEnumerable<GherkinToken> CreateTokens(this IEnumerable<string> rawlines)
        {
            var lineEnumerator = rawlines.GetEnumerator();
            while (lineEnumerator.MoveNext())
            {
                var line = lineEnumerator.Current;
                var type = IdentifyLineType(line);
                if (type == LineType.Example)
                {
                    lineEnumerator.MoveNext();
                    var nextLine = lineEnumerator.Current;
                    yield return new GherkinToken(type, line+nextLine);
                }
                else
                {
                    yield return new GherkinToken(type, line);
                }
            }
        }

        private static LineType IdentifyLineType(string line)
        {
            if (line.StartsWith("given", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Given;
            if (line.StartsWith("when", StringComparison.InvariantCultureIgnoreCase))
                return LineType.When;
            if (line.StartsWith("then", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Then;
            if (line.StartsWith("and", StringComparison.InvariantCultureIgnoreCase))
                return LineType.And;
            if (line.StartsWith("scenario outline", StringComparison.InvariantCultureIgnoreCase))
                return LineType.ScenarioOutline;
            if (line.StartsWith("scenario", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Scenario;
            if (line.StartsWith("example", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Example;
            if (line.StartsWith("|", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Data;
            return LineType.NOP;
        }
    }
}