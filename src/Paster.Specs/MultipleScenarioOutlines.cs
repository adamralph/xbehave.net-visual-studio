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
    [Trait("Multiple Scenarios/Scenario Outlines", "")]
    public class MultipleScenarioOutlines
    {
        [Scenario(DisplayName = "Scenario followed by scenario outline")]
        public void ScenarioThenScenarioOutline(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system"
                .Given(() =>
                {
                    environment = FakesLibrary.CreateDefaultEnvironment();
                    sut = new GherkinPaster(environment);
                });

            "And a gherkin snippet of a Scenario and a Scenario Outline"
                .And(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Scenario: Testing the gherkin paster");
                    sb.AppendLine();
                    sb.AppendLine("Scenario Outline: Testing the gherkin paster again");
                    sb.AppendLine("Examples:");
                    sb.AppendLine("| Test |");
                    sb.AppendLine("| one  |");
                    sb.AppendLine("| two  |");
                    clipboard = FakesLibrary.CreateShim(sb.ToString());
                });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the output should be a method wrapping 4 strings with appropriate extension methods"
                .Then(() =>
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("[Scenario]");
                    sb.AppendLine("public void TestingTheGherkinPaster()");
                    sb.AppendLine("{");
                    sb.AppendLine("}");
                    sb.AppendLine();
                    sb.AppendLine("[Scenario]");
                    sb.AppendLine("[Example(\"one\")]");
                    sb.AppendLine("[Example(\"two\")]");
                    sb.AppendLine("public void TestingTheGherkinPasterAgain(string Test)");
                    sb.AppendLine("{");
                    sb.AppendLine("}");
                    environment.TextWritten.Should()
                               .Be(sb.ToString());
                });
        }
    
    
        [Scenario(DisplayName = "Scenario outline followed by scenario")]
        public void ScenarioOutlineThenScenario(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system"
                .Given(() =>
                           {
                               environment = FakesLibrary.CreateDefaultEnvironment();
                               sut = new GherkinPaster(environment);
                           });

            "And a gherkin snippet of a Scenario and a Scenario Outline"
                .And(() =>
                         {
                             var sb = new StringBuilder();
                             sb.AppendLine("Scenario Outline: Testing the gherkin paster again");
                             sb.AppendLine("Examples:");
                             sb.AppendLine("| Test |");
                             sb.AppendLine("| one  |");
                             sb.AppendLine("| two  |");
                             sb.AppendLine();
                             sb.AppendLine("Scenario: Testing the gherkin paster");
                             clipboard = FakesLibrary.CreateShim(sb.ToString());
                         });

            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then the output should be a method wrapping 4 strings with appropriate extension methods"
                .Then(() =>
                          {
                              var sb = new StringBuilder();
                              sb.AppendLine("[Scenario]");
                              sb.AppendLine("[Example(\"one\")]");
                              sb.AppendLine("[Example(\"two\")]");
                              sb.AppendLine("public void TestingTheGherkinPasterAgain(string Test)");
                              sb.AppendLine("{");
                              sb.AppendLine("}");
                              sb.AppendLine();
                              sb.AppendLine("[Scenario]");
                              sb.AppendLine("public void TestingTheGherkinPaster()");
                              sb.AppendLine("{");
                              sb.AppendLine("}");
                              environment.TextWritten.Should()
                                         .Be(sb.ToString());
                          });
        }
    }
}
