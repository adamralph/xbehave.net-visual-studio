using System;

namespace xBehave.Paster.Gherkin
{
    internal abstract class TreeState
    {
        protected SyntaxTree Tree;

        public abstract TreeState Transition(Func<EmptyState, TreeState> stateEmpty,
                                             Func<ScenarioState, TreeState> stateScenario,
                                             Func<ImpliedScenarioState, TreeState> stateImpliedScenario,
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline,
                                             Func<ErrorState, TreeState> stateError);

        public override string ToString()
        {
            return Tree.ToString();
        }

        public abstract TreeState AddError(LineType lineType, string rawline);
    }
}