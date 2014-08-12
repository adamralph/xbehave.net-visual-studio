using System;

namespace xBehave.Paster.Gherkin
{
    internal class ImpliedScenarioState : TreeState, CanAddInstruction
    {
        private readonly CodelessGroupingNode _group;

        public ImpliedScenarioState(SyntaxTree tree, CodelessGroupingNode groupingNode)
        {
            _group = groupingNode;
            Tree = tree;
        }

        public override TreeState Transition(Func<EmptyState, TreeState> stateEmpty,
                                             Func<ScenarioState, TreeState> stateScenario,
                                             Func<ImpliedScenarioState, TreeState> stateImpliedScenario,
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline)
        {
            return stateImpliedScenario(this);
        }

        public TreeState AddInstruction(string rawLine, LineType rawType)
        {
            var node = new Instruction(rawLine, rawType);
            _group.AddNode(node);

            return this;
        }
    }
}