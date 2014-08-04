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

            currentState = source.GetText()
                                 .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => new {Type = GWTIdentify(s), RawLine = s})
                                 .Aggregate(currentState, (current, line) => _transitions[line.Type](current, line.RawLine));

            _environment.Paste(currentState.ToString());
        }

        private static Gherkin GWTIdentify(string line)
        {
            if (line.StartsWith("given", StringComparison.InvariantCultureIgnoreCase))
                return Gherkin.Given;
            if (line.StartsWith("when", StringComparison.InvariantCultureIgnoreCase))
                return Gherkin.When;
            if (line.StartsWith("then", StringComparison.InvariantCultureIgnoreCase))
                return Gherkin.Then;
            if (line.StartsWith("and", StringComparison.InvariantCultureIgnoreCase))
                return Gherkin.And;
            if (line.StartsWith("scenario", StringComparison.InvariantCultureIgnoreCase))
                return Gherkin.Scenario;
            return Gherkin.NOP;
        }

        private readonly IDictionary<Gherkin, Func<TreeState, string, TreeState>> _transitions =
            new Dictionary<Gherkin, Func<TreeState, string, TreeState>>
                {
                    {
                        Gherkin.Scenario,
                        (state, rawLine) =>
                        state.Transition(empty => empty.AddScenario(rawLine),
                                         scenario => scenario.AddScenario(rawLine),
                                         existing => existing)
                    },
                    {
                        Gherkin.Given,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Gherkin.Given),
                                         scenario => scenario.AddInstruction(rawline, Gherkin.Given),
                                         existing => existing.AddInstruction(rawline, Gherkin.Given))
                    },
                    {
                        Gherkin.When,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Gherkin.When),
                                         scenario => scenario.AddInstruction(rawline, Gherkin.When),
                                         existing => existing.AddInstruction(rawline, Gherkin.When))
                    },
                    {
                        Gherkin.Then,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Gherkin.Then),
                                         scenario => scenario.AddInstruction(rawline, Gherkin.Then),
                                         existing => existing.AddInstruction(rawline, Gherkin.Then))
                    },
                    {
                        Gherkin.And,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, Gherkin.And),
                                         scenario => scenario.AddInstruction(rawline, Gherkin.And),
                                         existing => existing.AddInstruction(rawline, Gherkin.And))
                    },
                    {
                        Gherkin.NOP, 
                        (state, rawline) => 
                        state.Transition(empty => empty, 
                                        scenario => scenario, 
                                        existing => existing)
                    }
                };
    }
}