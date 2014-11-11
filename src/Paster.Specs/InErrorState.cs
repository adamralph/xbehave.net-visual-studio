using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Paster.Specs.Fakes;
using Xbehave;
using xBehave.Paster.Gherkin;
using xBehave.Paster.System;
using Xunit;

namespace Paster.Specs
{
    [Trait("Unhappy Path","")]
    public class InErrorState
    {
        [Scenario(DisplayName = "When in an errored state, scenarios are recorded")]
        public void RecordingScenario(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system"
                .Given(() =>
                {
                    environment = FakesLibrary.CreateDefaultEnvironment();
                    sut = new GherkinPaster(environment);
                });

            "And a gherkin Scenario"
                .And(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Examples: Errant examples to cause error");
                    sb.AppendLine();
                    sb.AppendLine("Scenario: Testing the gherkin paster again");
                    sb.AppendLine("Given a line");
                    sb.AppendLine("And a line");
                    sb.AppendLine("When a line");
                    sb.AppendLine("Then a line");
                    clipboard = FakesLibrary.CreateShim(sb.ToString());
                });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the Scenario is recorded"
                .Then(() =>
                          environment.LinesWritten[1].Should().Be("//Scenario: Testing the gherkin paster again"));

        }
    }
}
