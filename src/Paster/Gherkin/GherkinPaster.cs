using System;
using System.Collections.Generic;
using System.Linq;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    public class GherkinPaster
    {
        private readonly IEnvironment _environment;

        public GherkinPaster(IEnvironment environment)
        {
            _environment = environment;
        }

        public void PasteGherkin(IClipboard source)
        {
            if (!source.ContainsText())
                return;

            TreeState currentState = new EmptyState();

            var gherkinTokens = source.GetText()
                                      .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => new {Type = GWTIdentify(s), RawLine = s});

            foreach (var line in gherkinTokens)
                currentState = Transitions[line.Type](currentState, line.RawLine);

            _environment.Paste(currentState.ToString());
        }

        private static Identifier GWTIdentify(string line)
        {
            if (line.StartsWith("given", StringComparison.InvariantCultureIgnoreCase))
                return Identifier.Given;
            if (line.StartsWith("when", StringComparison.InvariantCultureIgnoreCase))
                return Identifier.When;
            if (line.StartsWith("then", StringComparison.InvariantCultureIgnoreCase))
                return Identifier.Then;
            if (line.StartsWith("and", StringComparison.InvariantCultureIgnoreCase))
                return Identifier.And;
            if (line.StartsWith("scenario", StringComparison.InvariantCultureIgnoreCase))
                return Identifier.Scenario;
            return Identifier.NOP;
        }

        private readonly IDictionary<Identifier, Func<TreeState, string, TreeState>> Transitions =
            new Dictionary<Identifier, Func<TreeState, string, TreeState>>
                {
                    {
                        Identifier.Scenario,
                        (state, rawLine) =>
                        state.Transition(empty => empty.AddScenario(rawLine), scenario => scenario.AddScenario(rawLine), existing => existing)
                    },
                    {
                        Identifier.Given,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Identifier.Given),
                                         scenario => scenario.AddInstruction(rawline, Identifier.Given),
                                         existing => existing.AddInstruction(rawline, Identifier.Given))
                    },
                    {
                        Identifier.When,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Identifier.When),
                                         scenario => scenario.AddInstruction(rawline, Identifier.When),
                                         existing => existing.AddInstruction(rawline, Identifier.When))
                    },
                    {
                        Identifier.Then,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Identifier.Then),
                                         scenario => scenario.AddInstruction(rawline, Identifier.Then),
                                         existing => existing.AddInstruction(rawline, Identifier.Then))
                    },
                    {
                        Identifier.And,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Identifier.And),
                                         scenario => scenario.AddInstruction(rawline, Identifier.And),
                                         existing => existing.AddInstruction(rawline, Identifier.And))
                    },
                };
    }
}