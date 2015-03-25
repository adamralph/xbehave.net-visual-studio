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
    [Trait("Unhappy Path", "")]
    public class HandlingNOPLines
    {
        [Scenario(DisplayName = "When a Scenario: line is followed by a NOP line")]
        public void StrangeLineAfterScenario(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system"
                .Given(() =>
                {
                    environment = FakesLibrary.CreateDefaultEnvironment();
                    sut = new GherkinPaster(environment);
                });

            "And a gherkin Scenario that has a strange line "
                .And(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Scenario: Testing the gherkin paster");
                    sb.AppendLine("a line");
                    clipboard = FakesLibrary.CreateShim(sb.ToString());
                });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the output should be a method with an empty body"
                .Then(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("[Scenario]");
                    sb.AppendLine("public void TestingTheGherkinPaster()");
                    sb.AppendLine("{");
                    sb.AppendLine("}");
                    environment.TextWritten.Should()
                               .Be(sb.ToString());
                });
        }

        [Scenario(DisplayName = "When a Scenario outline: line is followed by a NOP line")]
        public void StrangeLineAfterScenarioOutline(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system"
                .Given(() =>
                {
                    environment = FakesLibrary.CreateDefaultEnvironment();
                    sut = new GherkinPaster(environment);
                });

            "And a gherkin Scenario that has a strange line "
                .And(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Scenario outline: Testing the gherkin paster");
                    sb.AppendLine("a line");
                    clipboard = FakesLibrary.CreateShim(sb.ToString());
                });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the output should be a method with an empty body"
                .Then(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("[Scenario]");
                    sb.AppendLine("public void TestingTheGherkinPaster()");
                    sb.AppendLine("{");
                    sb.AppendLine("}");
                    environment.TextWritten.Should()
                               .Be(sb.ToString());
                });
        }

        [Scenario(DisplayName = "When a isolated instruction line is followed by a NOP line")]
        public void InstructionFollowedByStrangeLine(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system"
                .Given(() =>
                {
                    environment = FakesLibrary.CreateDefaultEnvironment();
                    sut = new GherkinPaster(environment);
                });

            "And a gherkin Scenario that has a strange line "
                .And(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Given that i'm testing the gherkin paster");
                    sb.AppendLine("a line");
                    clipboard = FakesLibrary.CreateShim(sb.ToString());
                });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the output should be a method with an empty body"
                .Then(() =>
                          {
                              var expected = "\"Given that i'm testing the gherkin paster\".f(() => {});" + Environment.NewLine;
                              environment.TextWritten.Should()
                                         .Be(expected);
                          });
        }
    }
}
