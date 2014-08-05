using System;

namespace xBehave.Paster.Gherkin
{
    internal class ScenarioState : TreeState, CanAddScenario, CanAddInstruction
    {
        private Scenario _group;

        public ScenarioState(SyntaxTree tree, Scenario @group)
        {
            _group = @group;
            Tree = tree;
        }

        public override TreeState Transition(Func<EmptyState, TreeState> treeStateEmpty,
                                             Func<ScenarioState, TreeState> treeStateScenario,
                                             Func<ExistingScenarioState, TreeState> treeStateExistingScenario)
        {
            return treeStateScenario(this);
        }

        public TreeState AddScenario(string rawLine)
        {
            var text = rawLine.Trim()
                              .RemoveScenarioTag();
            _group = new Scenario(text);

            Tree.Add(_group);

            return this;
        }

        public TreeState AddInstruction(string rawLine, LineType rawType)
        {
            var node = new Instruction(rawLine, rawType);
            _group.AddNode(node);

            return this;
        }
    }
}