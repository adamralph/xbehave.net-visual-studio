using System.Linq;
using FluentAssertions;
using Paster.Specs.Fakes;
using Xbehave;
using xBehave.Paster.Gherkin;
using xBehave.Paster.System;

namespace Paster.Specs
{
    public class SimplePasting
    {
        [Scenario]
        public void SingleLinePaste(IClipboard clipboard,
                                    GherkinPaster sut,
                                    TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
            {
                environment = FakesLibrary.CreateDefaultEnvironment();
                sut = new GherkinPaster(environment);
            });
            "and a single line of valid gherkin".And(() => clipboard = FakesLibrary.CreateShim("Given a single line"));
            "When the single line is pasted".Then(() => sut.PasteGherkin(clipboard));
            "Then only a single line is received by the environment".Then(() => environment.LinesWritten.Count()
                                                                                           .Should()
                                                                                           .Be(1));
        }

        [Scenario]
        public void PastingDoubleQuotesInScenario(IClipboard clipboard,
                                                  GherkinPaster sut,
                                                  TestEnvironment environment)
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

        [Scenario]
        public void PAstingDoubleQuotesInGWTIClipboard(IClipboard clipboard,
                                                       GherkinPaster sut,
                                                       TestEnvironment environment)
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