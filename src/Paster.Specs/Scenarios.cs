﻿using System.Text;
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
        [Scenario(DisplayName = "Simple scenario with all four instruction types")]
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
                              sb.AppendLine("\"Given a line\".f(() => {});");
                              sb.AppendLine("\"And a line\".f(() => {});");
                              sb.AppendLine("\"When a line\".f(() => {});");
                              sb.AppendLine("\"Then a line\".f(() => {});");
                              sb.AppendLine("}");
                              environment.TextWritten.Should()
                                         .Be(sb.ToString());
                          });
        }


        [Scenario(DisplayName = "Multiple scenarios")]
        public void Multiplescenarios(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
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
                              sb.AppendLine("\"Given a line\".f(() => {});");
                              sb.AppendLine("\"And a line\".f(() => {});");
                              sb.AppendLine("\"When a line\".f(() => {});");
                              sb.AppendLine("\"Then a line\".f(() => {});");
                              sb.AppendLine("}");
                              sb.AppendLine();
                              sb.AppendLine("[Scenario]");
                              sb.AppendLine("public void TestingTheGherkinPasterAgain()");
                              sb.AppendLine("{");
                              sb.AppendLine("\"Given a line\".f(() => {});");
                              sb.AppendLine("\"And a line\".f(() => {});");
                              sb.AppendLine("\"When a line\".f(() => {});");
                              sb.AppendLine("\"Then a line\".f(() => {});");
                              sb.AppendLine("}");
                              environment.TextWritten.Should()
                                         .Be(sb.ToString());
                          });
        }

        [Trait("Unhappy Path", "")]
        [Scenario(DisplayName = "Can't add a scenario after grouping less gherkin")]
        public void CantAddScenario(EnvironmentClipboard clipboard, GherkinPaster sut, TestEnvironment environment)
        {
            "Given a complete system"
                .Given(() =>
                           {
                               environment = new TestEnvironment();
                               sut = new GherkinPaster(environment);
                           });

            "And a gherkin snippet of an instruction followed by a scenario"
                .And(() =>
                         {
                             var sb = new StringBuilder();
                             sb.AppendLine("Then an erroroneous line was copied");
                             sb.AppendLine();
                             sb.AppendLine("Scenario: Oh dear");

                             clipboard = new ClipboardShim(sb.ToString());
                         });


            "When the gherkin is pasted"
                .When(() => sut.PasteGherkin(clipboard));

            "Then there should be a comment saying the paste failed"
                .Then(() => environment.LinesWritten[1].Should()
                                                       .Be("//Error when pasting: Scenario: Oh dear"));
        }
    }
}