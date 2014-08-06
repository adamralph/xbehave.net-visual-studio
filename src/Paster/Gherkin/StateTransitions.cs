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
                        state.Transition(empty => empty.AddScenario(rawLine),
                                         scenario => scenario.AddScenario(rawLine),
                                         implied => implied,
                                         outline => outline.AddScenario(rawLine))
                    },
                    {
                        LineType.Given,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.Given),
                                         scenario => scenario.AddInstruction(rawline, LineType.Given),
                                         implied => implied.AddInstruction(rawline, LineType.Given),
                                         outline => outline.AddInstruction(rawline, LineType.Given))
                    },
                    {
                        LineType.When,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.When),
                                         scenario => scenario.AddInstruction(rawline, LineType.When),
                                         implied => implied.AddInstruction(rawline, LineType.When),
                                         outline => outline.AddInstruction(rawline, LineType.When))
                    },
                    {
                        LineType.Then,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.Then),
                                         scenario => scenario.AddInstruction(rawline, LineType.Then),
                                         implied => implied.AddInstruction(rawline, LineType.Then),
                                         outline => outline.AddInstruction(rawline, LineType.Then))
                    },
                    {
                        LineType.And,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddInstruction(rawline, LineType.And),
                                         scenario => scenario.AddInstruction(rawline, LineType.And),
                                         implied => implied.AddInstruction(rawline, LineType.And),
                                         outline => outline.AddInstruction(rawline, LineType.And))
                    },
                    {
                        LineType.NOP,
                        (state, rawline) => state.Transition(empty => empty, scenario => scenario, implied => implied, outline => outline)
                    }
                };
    }
}