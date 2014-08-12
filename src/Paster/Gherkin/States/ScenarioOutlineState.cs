using System;
using System.Linq;

namespace xBehave.Paster.Gherkin
{
    internal class ScenarioOutlineState : ScenarioState, CanAddData, CanAddExample
    {
        public ScenarioOutlineState(SyntaxTree tree, Scenario @group) : base(tree, @group)
        {}

        public override TreeState Transition(Func<EmptyState, TreeState> stateEmpty,
                                             Func<ScenarioState, TreeState> stateScenario,
                                             Func<ImpliedScenarioState, TreeState> stateImpliedScenario,
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline)
        {
            return stateScenarioOutline(this);
        }

        public TreeState AddExample(string rawline)
        {
            var variableNames = rawline.RemoveExampleTag()
                                       .Split(new[] {'|'}, StringSplitOptions.None)
                                       .Skip(1)
                                       .DropLast()
                                       .Select(n => n.Trim())
                                       .ToArray();
            Group.AddExample(variableNames);
            return this;
        }

        public TreeState AddData(string rawline)
        {
            var variableValues = rawline.Trim()
                                        .Split(new[] {'|'}, StringSplitOptions.None)
                                        .Skip(1)
                                        .DropLast()
                                        .Select(n => n.Trim())
                                        .ToArray();
            Group.AddData(variableValues);
            return this;
        }
    }
}