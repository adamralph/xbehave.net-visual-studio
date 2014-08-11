using System;
using System.Collections.Generic;
using System.Linq;

namespace xBehave.Paster.Gherkin
{
    internal class ScenarioOutlineState : ScenarioState, CanAddData, CanAddExample
    {
        public ScenarioOutlineState(SyntaxTree tree, Scenario @group) : base(tree, @group)
        {}

        public override TreeState Transition(Func<EmptyState, TreeState> stateEmpty,
                                             Func<ScenarioState, TreeState> stateScenario,
                                             Func<ImpliedScenarioState, TreeState> stateImpliedScenario,
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline)
        {
            return stateScenarioOutline(this);
        }

        public TreeState AddExample(string rawline)
        {
            var variableNames = rawline.RemoveExampleTag()
                                       .Split(new[] {'|'}, StringSplitOptions.None)
                                       .Skip(1)
                                       .DropLast()
                                       .Select(n => n.Trim())
                                       .ToArray();
            Group.AddExample(variableNames);
            return this;
        }

        public TreeState AddData(string rawline)
        {
            var variableValues = rawline.Trim()
                                        .Split(new[] {'|'}, StringSplitOptions.None)
                                        .Skip(1)
                                        .DropLast()
                                        .Select(n => n.Trim())
                                        .ToArray();
            Group.AddData(variableValues);
            return this;
        }
    }

    public static class EnumerableExtension
    {
        public static IEnumerable<T> DropLast<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return InternalDropLast(source);
        }

        private static IEnumerable<T> InternalDropLast<T>(IEnumerable<T> source)
        {
            T buffer = default(T);
            bool buffered = false;

            foreach (var x in source)
            {
                if (buffered)
                    yield return buffer;

                buffer = x;
                buffered = true;
            }
        }
    }
}