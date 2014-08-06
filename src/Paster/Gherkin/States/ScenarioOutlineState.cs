using System;

namespace xBehave.Paster.Gherkin
{
    internal class ScenarioOutlineState : ScenarioState, CanAddExample, CanAddData
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
            throw new NotImplementedException();
        }

        public TreeState AddData(string rawline)
        {
            throw new NotImplementedException();
        }
    }
}