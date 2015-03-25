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
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline,
                                             Func<ErrorState, TreeState> stateError)
        {
            return stateImpliedScenario(this);
        }

        public TreeState AddInstruction(string rawLine)
        {
            var node = new Instruction(rawLine);
            _group.AddNode(node);

            return this;
        }

        public override TreeState AddError(LineType lineType, string rawline)
        {
            return ErrorState.Create(Tree, "Can't be processed if the previous lines weren't part of a scenario", lineType, rawline);
        }
    }
}