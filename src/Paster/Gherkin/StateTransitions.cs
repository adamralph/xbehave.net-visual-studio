using System;
using System.Collections.Generic;

namespace xBehave.Paster.Gherkin
{
    internal static class StateTransitions
    {
        private static readonly Func<LineType, TreeState, string, TreeState> InstructionTransition =
            (type, state, rawline) =>
            state.Transition(empty => empty.AddInstruction(rawline, type),
                             scenario => scenario.AddInstruction(rawline, type),
                             implied => implied.AddInstruction(rawline, type),
                             outline => outline.AddInstruction(rawline, type),
                             error => error.AddError(LineType.Given, rawline));

        internal static IDictionary<LineType, Func<TreeState, string, TreeState>> Transition =
            new Dictionary<LineType, Func<TreeState, string, TreeState>>
                {
                    {
                        LineType.Scenario,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddScenario(rawline),
                                         scenario => scenario.AddScenario(rawline),
                                         implied => implied.AddError(LineType.Scenario, rawline),
                                         outline => outline.AddScenario(rawline),
                                         error => error.AddError(LineType.Scenario, rawline))
                    },
                    {
                        LineType.ScenarioOutline,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddScenarioOutline(rawline),
                                         scenario => scenario.AddScenarioOutline(rawline),
                                         implied => implied.AddError(LineType.ScenarioOutline, rawline),
                                         outline => outline.AddScenarioOutline(rawline),
                                         error => error.AddError(LineType.ScenarioOutline, rawline))
                    },
                    {LineType.Given, (state, rawline) => InstructionTransition(LineType.Given, state, rawline)},
                    {LineType.When, (state, rawline) => InstructionTransition(LineType.When, state, rawline)},
                    {LineType.Then, (state, rawline) => InstructionTransition(LineType.Then, state, rawline)},
                    {LineType.And, (state, rawline) => InstructionTransition(LineType.And, state, rawline)},
                    {
                        LineType.Example,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddError(LineType.Example, rawline),
                                         scenario => scenario.AddError(LineType.Example, rawline),
                                         implied => implied.AddError(LineType.Example, rawline),
                                         outline => outline.AddExample(rawline),
                                         error => error.AddError(LineType.Example, rawline))
                    },
                    {
                        LineType.Data,
                        (state, rawline) =>
                        state.Transition(empty => empty.AddError(LineType.Data, rawline),
                                         scenario => scenario.AddError(LineType.Data, rawline),
                                         implied => implied.AddError(LineType.Data, rawline),
                                         outline => outline.AddData(rawline),
                                         error => error.AddError(LineType.Data, rawline))
                    },
                    {
                        LineType.NOP,
                        (state, rawline) =>
                        state.Transition(empty => empty, scenario => scenario, implied => implied, outline => outline, error => error)
                    }
                };
    }
}