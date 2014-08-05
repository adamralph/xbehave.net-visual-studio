using System.Linq;
using FluentAssertions;
using Paster.Specs.Fakes;
using Xbehave;
using xBehave.Paster.Gherkin;
using xBehave.Paster.System;
using Xunit;

namespace Paster.Specs
{
    [Trait("Escape special characters", "")]
    public class EscapeCharacters
    {

        [Scenario(DisplayName = "Handle scenario desciptions with double quotes")]
        public void PastingDoubleQuotesInScenario(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
                                                {
                                                    environment = FakesLibrary.CreateDefaultEnvironment();
                                                    sut = new GherkinPaster(environment);
                                                });
            "and a scenario containing a 'double quote'".And(() => clipboard = FakesLibrary.CreateShim("Scenario: With a \" character"));
            "When the single line is pasted".Then(() => sut.PasteGherkin(clipboard));
            "Then the 'double quote' character has been removed".Then(() => environment.LinesWritten.Any(l => l.Contains("\""))
                                                                                       .Should()
                                                                                       .BeFalse());
        }

        [Scenario(DisplayName = "Handle instruction with double quotes")]
        public void PastingDoubleQuotesInInstructions(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
                                                {
                                                    environment = FakesLibrary.CreateDefaultEnvironment();
                                                    sut = new GherkinPaster(environment);
                                                });
            "and a line containing a 'double quote'".And(() => clipboard = FakesLibrary.CreateShim("Given a \" character"));
            "When the line is pasted".Then(() => sut.PasteGherkin(clipboard));
            "then the 'double quote' character has been replaced by the 'backlash' and 'boudle quote' characters".Then(() =>
                                                                                                                           {
                                                                                                                               environment.LinesWritten[0].Should()
                                                                                                                                                          .Be("\"Given a \\\" character\".Given(() => {});");
                                                                                                                           });
        }
    }
}