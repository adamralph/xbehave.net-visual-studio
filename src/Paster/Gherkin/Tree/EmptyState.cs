using System;

namespace xBehave.Paster.Gherkin
{
    internal class EmptyState : TreeState, CanAddInstruction, CanAddScenario
    {
        public EmptyState()
        {
            _tree = new SyntaxTree();
        }

        public override TreeState Transition(Func<EmptyState, TreeState> treeStateEmpty,
                                             Func<ScenarioState, TreeState> treeStateScenario,
                                             Func<ExistingScenarioState, TreeState> treeStateExistingScenario)
        {
            return treeStateEmpty(this);
        }

        public TreeState AddInstruction(string rawLine, Identifier rawType)
        {
            var node = new GWTLine(rawLine, rawType);
            _tree.AddNode(node);
            return new ExistingScenarioState(_tree);
        }

        public TreeState AddScenario(string rawLine)
        {
            var node = new ScenarioLine(rawLine);
            _tree.AddParent(node);
            return new ScenarioState(_tree);
        }
    }
}