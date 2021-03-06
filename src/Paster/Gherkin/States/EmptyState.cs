using System;

namespace xBehave.Paster.Gherkin
{
    internal class EmptyState : TreeState, CanAddInstruction, CanAddScenario, CanAddScenarioOutline
    {
        public EmptyState()
        {
            Tree = new SyntaxTree();
        }

        public override TreeState Transition(Func<EmptyState, TreeState> stateEmpty,
                                             Func<ScenarioState, TreeState> stateScenario,
                                             Func<ImpliedScenarioState, TreeState> stateImpliedScenario,
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline,
                                             Func<ErrorState, TreeState> stateError)
        {
            return stateEmpty(this);
        }

        public override TreeState AddError(LineType lineType, string rawline)
        {
            var reason = String.Format("Can't a {0} when it's the first line seen", lineType);
            return ErrorState.Create(Tree, reason, lineType, rawline);
        }

        public TreeState AddInstruction(string rawLine)
        {
            var group = new CodelessGroupingNode();
            var node = new Instruction(rawLine);
            group.AddNode(node);
            Tree.Add(group);
            return new ImpliedScenarioState(Tree, group);
        }

        public TreeState AddScenario(string rawLine)
        {
            var group = new Scenario(rawLine.RemoveScenarioTag());
            Tree.Add(group);
            return new ScenarioState(Tree, group);
        }

        public TreeState AddScenarioOutline(string rawLine)
        {
            var group = new Scenario(rawLine.RemoveScenarioOutlineTag());
            Tree.Add(group);
            return new ScenarioOutlineState(Tree, group);
        }
    }
}