using System;

namespace xBehave.Paster.Gherkin
{
    internal class ScenarioState : TreeState, CanAddScenario, CanAddInstruction, CanAddScenarioOutline
    {
        protected Scenario _group;

        public ScenarioState(SyntaxTree tree, Scenario @group)
        {
            _group = @group;
            Tree = tree;
        }

        public override TreeState Transition(Func<EmptyState, TreeState> stateEmpty,
                                             Func<ScenarioState, TreeState> stateScenario,
                                             Func<ImpliedScenarioState, TreeState> stateImpliedScenario,
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline)
        {
            return stateScenario(this);
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

        public TreeState AddScenarioOutline(string rawLine)
        {
            var text = rawLine.Trim()
                              .RemoveScenarioTag();
            _group = new Scenario(text);

            Tree.Add(_group);

            return new ScenarioOutlineState(Tree, _group);
        }
    }
}