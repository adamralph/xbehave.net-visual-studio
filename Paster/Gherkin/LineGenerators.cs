using System;
using System.Collections.Generic;

namespace xBehave.Paster.Gherkin
{
    internal static class LineGenerators
    {
        private static readonly Dictionary<Identifier, Func<string, dynamic>> CSharpGenerators = new Dictionary<Identifier, Func<string, dynamic>>
        {
            {
                Identifier.Given, line => new GWTLine(line,
                                                      Identifier.Given)
            },
            {
                Identifier.When, line => new GWTLine(line,
                                                     Identifier.When)
            },
            {
                Identifier.Then, line => new GWTLine(line,
                                                     Identifier.Then)
            },
            {
                Identifier.And, line => new GWTLine(line,
                                                    Identifier.And)
            },
            {Identifier.Scenario, line => new ScenarioLine(line)},
            {Identifier.NOP, line => new NOPLine()},
        };

        public static IDictionary<Identifier, Func<string, dynamic>> CSharp
        {
            get { return CSharpGenerators; }
        }
    }
}