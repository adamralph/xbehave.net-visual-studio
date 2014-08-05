using System;

namespace xBehave.Paster.Gherkin
{
    internal class ExistingScenarioState : TreeState, CanAddInstruction
    {
        private readonly CodelessGroupingNode _group;

        public ExistingScenarioState(SyntaxTree tree, CodelessGroupingNode groupingNode)
        {
            _group = groupingNode;
            Tree = tree;
        }

        public override TreeState Transition(Func<EmptyState, TreeState> treeStateEmpty,
                                             Func<ScenarioState, TreeState> treeStateScenario,
                                             Func<ExistingScenarioState, TreeState> treeStateExistingScenario)
        {
            return treeStateExistingScenario(this);
        }

        public TreeState AddInstruction(string rawLine, LineType rawType)
        {
            var node = new Instruction(rawLine, rawType);
            _group.AddNode(node);

            return this;
        }
    }
}