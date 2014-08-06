using System;
using System.Collections.Generic;

namespace xBehave.Paster.Gherkin
{
    internal static class StateTransitions
    {
        internal static IDictionary<LineType, Func<TreeState, string, TreeState>> Transition =
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