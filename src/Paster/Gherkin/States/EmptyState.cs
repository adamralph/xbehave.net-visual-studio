using System;

namespace xBehave.Paster.Gherkin
{
    internal class EmptyState : TreeState, CanAddInstruction, CanAddScenario
    {
        public EmptyState()
        {
            Tree = new SyntaxTree();
        }

        public override TreeState Transition(Func<EmptyState, TreeState> treeStateEmpty,
                                             Func<ScenarioState, TreeState> treeStateScenario,
                                             Func<ExistingScenarioState, TreeState> treeStateExistingScenario)
        {
            return treeStateEmpty(this);
        }

        public TreeState AddInstruction(string rawLine, LineType rawType)
        {
            var group = new CodelessGroupingNode();
            var node = new Instruction(rawLine, rawType);
            group.AddNode(node);
            Tree.Add(group);
            return new ExistingScenarioState(Tree, group);
        }

        public TreeState AddScenario(string rawLine)
        {
            var group = new Scenario(rawLine.RemoveScenarioTag());
            Tree.Add(group);
            return new ScenarioState(Tree, group);
        }
    }
}