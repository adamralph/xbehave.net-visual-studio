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
        [Scenario(DisplayName = "Scenario outline with two examples and all four instructions")]
        public void TwoExamples(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment, StringBuilder stringBuilder)
        {
            "Given a complete system"
                .Given(() =>
                           {
                               environment = FakesLibrary.CreateDefaultEnvironment();
                               sut = new GherkinPaster(environment);
                           });

            "And a gherkin scenario outline"
                .And(() =>
                         {
                             stringBuilder = new StringBuilder();
                             stringBuilder.AppendLine("Scenario Outline: eating");
                             stringBuilder.AppendLine("Given there are <start> cucumbers");
                             stringBuilder.AppendLine("And I am feeling hungry for cucumbers");
                             stringBuilder.AppendLine("When I eat <eat> cucumbers");
                             stringBuilder.AppendLine("Then I should have <left> cucumbers");
                             stringBuilder.AppendLine("Examples:");
                             stringBuilder.AppendLine("| start  | eat | left  |");
                             stringBuilder.AppendLine("| Twelve |  5  |  7.0  |");
                             stringBuilder.AppendLine("| Twenty |  5  |  15.0 |");

                             clipboard = FakesLibrary.CreateShim(stringBuilder.ToString());
                         });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the first line emitted is the Scenario attribute"
                .Then(() => environment.LinesWritten[0].Should()
                                                       .Be("[Scenario]"));

            "Then the second and third lines emitted should be the Example attribute"
                .Then(() =>
                          {
                              environment.LinesWritten[1].Should()
                                                         .Be("[Example(\"Twelve\",5,7.0)]");
                              environment.LinesWritten[2].Should()
                                                         .Be("[Example(\"Twenty\",5,15.0)]");
                          });

            "Then the fourth line emitted is the method name with parameters"
                .Then(() => environment.LinesWritten[3].Should()
                                                       .Be("public void Eating(string start, int eat, double left)"));

            "Then the instructions emitted have had their placeholders replaced"
                .Then(() =>
                          {
                              environment.LinesWritten[5].Should()
                                                         .Be("\"Given there are {0} cucumbers\".f(() => {});");
                              environment.LinesWritten[6].Should()
                                                         .Be("\"And I am feeling hungry for cucumbers\".f(() => {});");
                              environment.LinesWritten[7].Should()
                                                         .Be("\"When I eat {1} cucumbers\".f(() => {});");
                              environment.LinesWritten[8].Should()
                                                         .Be("\"Then I should have {2} cucumbers\".f(() => {});");
                          });

            "Then the environment receives a test method with examples"
                .Then(() =>
                          {
                              var sb = new StringBuilder();

                              sb.AppendLine("[Scenario]");
                              sb.AppendLine("[Example(\"Twelve\",5,7.0)]");
                              sb.AppendLine("[Example(\"Twenty\",5,15.0)]");
                              sb.AppendLine("public void Eating(string start, int eat, double left)");
                              sb.AppendLine("{");
                              sb.AppendLine("\"Given there are {0} cucumbers\".f(() => {});");
                              sb.AppendLine("\"And I am feeling hungry for cucumbers\".f(() => {});");
                              sb.AppendLine("\"When I eat {1} cucumbers\".f(() => {});");
                              sb.AppendLine("\"Then I should have {2} cucumbers\".f(() => {});");
                              sb.AppendLine("}");

                              environment.TextWritten.Should()
                                         .Be(sb.ToString());
                          });
        }

        [Scenario(DisplayName = "Multiple Scenario outlines")]
        public void MultipleScenarioOutlines(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment, StringBuilder stringBuilder)
        {
            "Given a complete system"
                .Given(() =>
                {
                    environment = FakesLibrary.CreateDefaultEnvironment();
                    sut = new GherkinPaster(environment);
                });

            "And a gherkin scenario outline"
                .And(() =>
                {
                    stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Scenario Outline: eating");
                    stringBuilder.AppendLine("Examples:");
                    stringBuilder.AppendLine("| eat |");
                    stringBuilder.AppendLine("| 5   |");
                    stringBuilder.AppendLine("Scenario Outline: still eating");
                    stringBuilder.AppendLine("Examples:");
                    stringBuilder.AppendLine("| left  |");
                    stringBuilder.AppendLine("|  7.0  |");

                    clipboard = FakesLibrary.CreateShim(stringBuilder.ToString());
                });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the environment receives a test method with examples"
                .Then(() =>
                {
                    var sb = new StringBuilder();

                    sb.AppendLine("[Scenario]");
                    sb.AppendLine("[Example(5)]");
                    sb.AppendLine("public void Eating(int eat)");
                    sb.AppendLine("{");
                    sb.AppendLine("}");
                    sb.AppendLine();
                    sb.AppendLine("[Scenario]");
                    sb.AppendLine("[Example(7.0)]");
                    sb.AppendLine("public void StillEating(double left)");
                    sb.AppendLine("{");
                    sb.AppendLine("}");
                    environment.TextWritten.Should()
                               .Be(sb.ToString());
                });
        }

    }
}