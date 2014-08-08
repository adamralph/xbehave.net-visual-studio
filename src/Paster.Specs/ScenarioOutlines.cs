using System.Text;
using FluentAssertions;
using Paster.Specs.Fakes;
using Xbehave;
using xBehave.Paster.Gherkin;
using xBehave.Paster.System;
using Xunit;

namespace Paster.Specs
{
    [Trait("Scenario Outlines", "")]
    public class ScenarioOutlines
    {
        [Scenario(DisplayName = "Scenario outline with two examples")]
        public void TwoExamples(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
                                                {
                                                    environment = FakesLibrary.CreateDefaultEnvironment();
                                                    sut = new GherkinPaster(environment);
                                                });

            "And a gherkin scenario outline".And(() =>
                                                     {
                                                         var sb = new StringBuilder();
                                                         sb.AppendLine("Scenario Outline: eating");
                                                         sb.AppendLine("Given there are <start> cucumbers");
                                                         sb.AppendLine("When I eat <eat> cucumbers");
                                                         sb.AppendLine("Then I should have <left> cucumbers");
                                                         sb.AppendLine("Examples:");
                                                         sb.AppendLine("| start | eat | left |");
                                                         sb.AppendLine("|  12   |  5  |  7   |");
                                                         sb.AppendLine("|  20   |  5  |  15  |");

                                                         clipboard = FakesLibrary.CreateShim(sb.ToString());
                                                     });

            "When the gherkin is pasted".When(() => sut.PasteGherkin(clipboard));

            "Then the environment receives a test method with examples"
                .Then(() =>
                          {
                              var sb = new StringBuilder();

                              sb.AppendLine("[Scenario]");
                              sb.AppendLine("[Example(12,5,7)]");
                              sb.AppendLine("[Example(20,5,15)]");
                              sb.AppendLine("public void Eating(int start, int eat, int left)");
                              sb.AppendLine("{");
                              sb.AppendLine("\"Given there are {0} cucumbers\".Given(() => {});");
                              sb.AppendLine("\"When I eat {1} cucumbers\".When(() => {});");
                              sb.AppendLine("\"Then I should have {2} cucumbers\".Then(() => {});");
                              sb.AppendLine("}");

                              environment.TextWritten.Should()
                                         .Be(sb.ToString());
                          });

            "Then the first line emitted is the Scenario attribute"
                .Then(() => environment.LinesWritten[0].Should()
                                                       .Be("[Scenario]"));

            "Then the second and third lines emitted should be the Example attribute"
                .Then(() =>
                          {
                              environment.LinesWritten[1].Should()
                                                         .Be("[Example(12,5,7)]");
                              environment.LinesWritten[2].Should()
                                                         .Be("[Example(20,5,15)]");
                          });

            "Then the fourth line emitted is the method name with parameters"
                .Then(() => environment.LinesWritten[3].Should()
                                                       .Be("public void Eating(int start, int eat, int left)"));

            "Then the instructions emitted have had their placeholders replaced"
                .Then(() =>
                          {
                              environment.LinesWritten[5].Should()
                                                         .Be("\"Given there are {0} cucumbers\".Given(() => {});");
                              environment.LinesWritten[6].Should()
                                                         .Be("\"When I eat {1} cucumbers\".When(() => {});");
                              environment.LinesWritten[7].Should()
                                                         .Be("\"Then I should have {2} cucumbers\".Then(() => {});");
                          });
        }
    }
}