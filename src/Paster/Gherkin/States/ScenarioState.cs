using System;

namespace xBehave.Paster.Gherkin
{
    internal class ScenarioState : TreeState, CanAddScenario, CanAddInstruction, CanAddScenarioOutline
    {
        protected Scenario Group;

        public ScenarioState(SyntaxTree tree, Scenario @group)
        {
            Group = @group;
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
            Group = new Scenario(text);

            Tree.Add(Group);

            return this;
        }

        public TreeState AddInstruction(string rawLine, LineType rawType)
        {
            var node = new Instruction(rawLine, rawType);
            Group.AddNode(node);

            return this;
        }

        public TreeState AddScenarioOutline(string rawLine)
        {
            var text = rawLine.Trim()
                              .RemoveScenarioTag();
            Group = new Scenario(text);

            Tree.Add(Group);

            return new ScenarioOutlineState(Tree, Group);
        }
    }
}