using System.Text;
using FluentAssertions;
using Paster.Specs.Fakes;
using Xbehave;
using xBehave.Paster.Gherkin;
using xBehave.Paster.System;
using Xunit;

namespace Paster.Specs
{
    [Trait("Scenarios","")]
    public class Scenarios
    {
        [Scenario(DisplayName = "Expected default use case")]
        public void DefaultUseCase(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
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
                             sb.AppendLine("Scenario: Testing the gherkin paster");
                             sb.AppendLine("Given a line");
                             sb.AppendLine("And a line");
                             sb.AppendLine("When a line");
                             sb.AppendLine("Then a line");
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
                              sb.AppendLine("\"Given a line\".Given(() => {});");
                              sb.AppendLine("\"And a line\".And(() => {});");
                              sb.AppendLine("\"When a line\".When(() => {});");
                              sb.AppendLine("\"Then a line\".Then(() => {});");
                              sb.AppendLine("}");
                              environment.TextWritten.Should()
                                         .Be(sb.ToString());
                          });
        }


        [Scenario(DisplayName = "Multiple scenarios")]
        public void multiplescenarios(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "given a complete system"
                .Given(() =>
                           {
                               environment = FakesLibrary.CreateDefaultEnvironment();
                               sut = new GherkinPaster(environment);
                           });

            "And a gherkin Scenario"
                .And(() =>
                         {
                             var sb = new StringBuilder();
                             sb.AppendLine("Scenario: Testing the gherkin paster");
                             sb.AppendLine("Given a line");
                             sb.AppendLine("And a line");
                             sb.AppendLine("When a line");
                             sb.AppendLine("Then a line");
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

            "Then the output should be a method wrapping 4 strings with appropriate extension methods"
                .Then(() =>
                          {
                              var sb = new StringBuilder();
                              sb.AppendLine("[Scenario]");
                              sb.AppendLine("public void TestingTheGherkinPaster()");
                              sb.AppendLine("{");
                              sb.AppendLine("\"Given a line\".Given(() => {});");
                              sb.AppendLine("\"And a line\".And(() => {});");
                              sb.AppendLine("\"When a line\".When(() => {});");
                              sb.AppendLine("\"Then a line\".Then(() => {});");
                              sb.AppendLine("}");
                              sb.AppendLine();
                              sb.AppendLine("[Scenario]");
                              sb.AppendLine("public void TestingTheGherkinPasterAgain()");
                              sb.AppendLine("{");
                              sb.AppendLine("\"Given a line\".Given(() => {});");
                              sb.AppendLine("\"And a line\".And(() => {});");
                              sb.AppendLine("\"When a line\".When(() => {});");
                              sb.AppendLine("\"Then a line\".Then(() => {});");
                              sb.AppendLine("}");
                              environment.TextWritten.Should()
                                         .Be(sb.ToString());
                          });
        }
    }
}