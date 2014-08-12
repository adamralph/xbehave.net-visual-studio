using System;
using System.Linq;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    public class GherkinPaster
    {
        private readonly DevelopmentEnvironment _environment;

        public GherkinPaster(DevelopmentEnvironment environment)
        {
            _environment = environment;
        }

        public void PasteGherkin(EnvironmentClipboard source)
        {
            if (!source.ContainsText())
                return;

            TreeState currentState = new EmptyState();
            var tokens = source.GetText()
                               .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                               .CreateTokens().ToList();
            currentState = tokens.Aggregate(currentState, (current, line) => StateTransitions.Transition[line.Type](current, line.RawLine));

            _environment.Paste(currentState.ToString());
        }
    }
}