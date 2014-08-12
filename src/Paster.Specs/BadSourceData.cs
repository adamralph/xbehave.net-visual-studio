using System;
using System.Linq;
using FluentAssertions;
using Paster.Specs.Fakes;
using Xbehave;
using xBehave.Paster.Gherkin;
using xBehave.Paster.System;
using Xunit;

namespace Paster.Specs
{
    [Trait("Invalid gherkin","")]
    public class BadSourceData
    {
        [Scenario(DisplayName = "Pasting an Empty string")]
        public void EmptyString(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
            {
                environment = FakesLibrary.CreateDefaultEnvironment();
                sut = new GherkinPaster(environment);
            });

            "and a empty string "
                .And(() => clipboard = FakesLibrary.CreateShim(String.Empty));

            "When the text is pasted"
                .Then(() => sut.PasteGherkin(clipboard));

            "Then no lines are received by the environment"
                .Then(() => environment.LinesWritten.Count()
                                       .Should()
                                       .Be(0));
        }

        [Scenario(DisplayName = "Pasting invalid gherkin")]
        public void InvalidGherkin(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
            {
                environment = FakesLibrary.CreateDefaultEnvironment();
                sut = new GherkinPaster(environment);
            });

            "and an invalid string of gherkin"
                .And(() => clipboard = FakesLibrary.CreateShim("This is not valid gherkin"));

            "When the text is pasted"
                .Then(() => sut.PasteGherkin(clipboard));

            "Then no lines are received by the environment"
                .Then(() => environment.LinesWritten.Count()
                                       .Should()
                                       .Be(0));
        }
    }
}