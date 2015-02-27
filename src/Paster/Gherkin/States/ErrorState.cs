using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    internal class ErrorState : TreeState
    {
        public override TreeState Transition(Func<EmptyState, TreeState> stateEmpty,
                                             Func<ScenarioState, TreeState> stateScenario,
                                             Func<ImpliedScenarioState, TreeState> stateImpliedScenario,
                                             Func<ScenarioOutlineState, TreeState> stateScenarioOutline,
                                             Func<ErrorState, TreeState> stateError)
        {
            return stateError(this);
        }

        private readonly List<string> _errors = new List<string>();
        protected string InitialReason { get; private set; }

        public override TreeState AddError(LineType lineType, string rawline)
        {
            _errors.Add(rawline);
            return this;
        }

        public static TreeState Create(SyntaxTree successes, string reason, LineType lineType, string rawline)
        {
            var state = new ErrorState {InitialReason = reason, Tree = successes};
            return state.AddError(lineType, rawline);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Tree.ToString());

            var firstError = _errors.Take(1)
                                    .Single();
            sb.AppendFormat("//Error when pasting: {0}{1}", firstError, Environment.NewLine);
            sb.AppendFormat("//Reason: {0}{1}", InitialReason, Environment.NewLine);
            sb.AppendLine();

            var subsequentLines = _errors.Skip(1)
                                         .Select(e => String.Format("//{0}", e));

            sb.AppendLines(subsequentLines);

            return sb.ToString();
        }
    }
}