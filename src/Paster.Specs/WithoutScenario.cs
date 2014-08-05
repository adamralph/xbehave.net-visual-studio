using System.Linq;
using System.Text;
using FluentAssertions;
using Paster.Specs.Fakes;
using Xbehave;
using xBehave.Paster.Gherkin;
using xBehave.Paster.System;
using Xunit;

namespace Paster.Specs
{
    [Trait("Without a scenario", "")]
    public class WithoutScenario
    {
        [Scenario(DisplayName = "Handle instructions without a scenario")]
        public void ScenarioLessPaste(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
                                                {
                                                    environment = FakesLibrary.CreateDefaultEnvironment();
                                                    sut = new GherkinPaster(environment);
                                                });
            "and a GWT snippet".And(() =>
                                        {
                                            var sb = new StringBuilder();
                                            sb.AppendLine("Given a line");
                                            sb.AppendLine("And a line");
                                            sb.AppendLine("When a line");
                                            sb.AppendLine("Then a line");
                                            clipboard = FakesLibrary.CreateShim(sb.ToString());
                                        });
            "When the snippet is pasted".Then(() => sut.PasteGherkin(clipboard));
            "Then only four expected lines are received by the environment".Then(() => environment.LinesWritten.Count()
                                                                                                  .Should()
                                                                                                  .Be(4));
        }
    }
}