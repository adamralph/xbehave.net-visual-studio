using System;

namespace xBehave.Paster.Gherkin
{
    internal class ExistingScenarioState : TreeState, CanAddInstruction
    {
        public ExistingScenarioState(SyntaxTree tree)
        {
            _tree = tree;
        }

        public override TreeState Transition(Func<EmptyState, TreeState> treeStateEmpty,
                                             Func<ScenarioState, TreeState> treeStateScenario,
                                             Func<ExistingScenarioState, TreeState> treeStateExistingScenario)
        {
            return treeStateExistingScenario(this);
        }

        public TreeState AddInstruction(string rawLine, Identifier rawType)
        {
            var node = new GWTLine(rawLine, rawType);
            _tree.AddNode(node);
            return this;
        }
    }
}