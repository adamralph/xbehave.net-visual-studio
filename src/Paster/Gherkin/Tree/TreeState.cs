using System;

namespace xBehave.Paster.Gherkin
{
    internal abstract class TreeState
    {
        protected SyntaxTree _tree;

        public abstract TreeState Transition(Func<EmptyState, TreeState> treeStateEmpty,
                                             Func<ScenarioState, TreeState> treeStateScenario,
                                             Func<ExistingScenarioState, TreeState> treeStateExistingScenario);

        public override string ToString()
        {
            return _tree.ToString();
        }
    }
}