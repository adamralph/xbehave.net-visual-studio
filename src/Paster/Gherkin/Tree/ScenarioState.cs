using System;

namespace xBehave.Paster.Gherkin
{
    internal class ScenarioState : TreeState, CanAddScenario, CanAddInstruction
    {
        public ScenarioState(SyntaxTree tree)
        {
            _tree = tree;
        }

        public override TreeState Transition(Func<EmptyState, TreeState> treeStateEmpty,
                                             Func<ScenarioState, TreeState> treeStateScenario,
                                             Func<ExistingScenarioState, TreeState> treeStateExistingScenario)
        {
            return treeStateScenario(this);
        }

        public TreeState AddScenario(string rawLine)
        {
            var node = new ScenarioLine(rawLine);
            _tree.AddParent(node);
            return this;
        }

        public TreeState AddInstruction(string rawLine, Identifier rawType)
        {
            var node = new GWTLine(rawLine, rawType);
            _tree.AddNode(node);
            return this;
        }
    }
}