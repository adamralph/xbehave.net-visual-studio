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
    }
}