using System;
using System.Collections.Generic;
using System.Linq;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    public class GherkinPaster
    {
        private readonly DevelopmentEnvironment _environment;

        public GherkinPaster(DevelopmentEnvironment environment)
        {
            _environment = environment;
        }

        public void PasteGherkin(EnvironmentClipboard source)
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

        private static LineType GWTIdentify(string line)
        {
            if (line.StartsWith("given", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Given;
            if (line.StartsWith("when", StringComparison.InvariantCultureIgnoreCase))
                return LineType.When;
            if (line.StartsWith("then", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Then;
            if (line.StartsWith("and", StringComparison.InvariantCultureIgnoreCase))
                return LineType.And;
            if (line.StartsWith("scenario", StringComparison.InvariantCultureIgnoreCase))
                return LineType.Scenario;
            return LineType.NOP;
        }

        private readonly IDictionary<LineType, Func<TreeState, string, TreeState>> _transitions =
            new Dictionary<LineType, Func<TreeState, string, TreeState>>
                {
                    {
                        LineType.Scenario,
                        (state, rawLine) =>
                        state.Transition(empty => empty.AddScenario(rawLine), scenario => scenario.AddScenario(rawLine), existing => existing)
                    },
                    {
                        LineType.Given,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.Given),
                                         scenario => scenario.AddInstruction(rawline, LineType.Given),
                                         existing => existing.AddInstruction(rawline, LineType.Given))
                    },
                    {
                        LineType.When,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.When),
                                         scenario => scenario.AddInstruction(rawline, LineType.When),
                                         existing => existing.AddInstruction(rawline, LineType.When))
                    },
                    {
                        LineType.Then,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.Then),
                                         scenario => scenario.AddInstruction(rawline, LineType.Then),
                                         existing => existing.AddInstruction(rawline, LineType.Then))
                    },
                    {
                        LineType.And,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.And),
                                         scenario => scenario.AddInstruction(rawline, LineType.And),
                                         existing => existing.AddInstruction(rawline, LineType.And))
                    },
                    {LineType.NOP, (state, rawline) => state.Transition(empty => empty, scenario => scenario, existing => existing)}
                };
    }
}