using System.Linq;
using System.Text;
using FluentAssertions;
using Paster.Specs.Fakes;
using SiliconSharkLtd.Paster;
using Xbehave;

namespace Paster.Specs
{
    public class ScenarioPasting
    {
        [Scenario]
        public void SimpleGWTLines(IClipboard clipboard,
                                   GherkinPaster sut,
                                   TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
            {
                environment = FakesLibrary.CreateDefaultEnvironment();
                sut = new GherkinPaster(environment);
            });
            "And a 'Scenario' snippet of gherkin".And(() =>
            {
                var sb = new StringBuilder();
                sb.AppendLine("Scenario: Pasting gherkin using the paster addin");
                clipboard = FakesLibrary.CreateShim(sb.ToString());
            });
            "When the gherkin is pasted".Then(() => sut.PasteGherkin(clipboard));
            "Then the output is on four lines".Then(() => environment.LinesWritten.Count()
                                                                     .Should()
                                                                     .Be(4));
            "Then the first list should be the scenario attribute".Then(() => environment.LinesWritten[0].Should()
                                                                                                         .Be("[Scenario]"));
            "Then the second line should be 'public void PastingGherkinUsingThePasterAddin()'".Then(() => environment.LinesWritten[1].Should()
                                                                                                                                    .Be("public void PastingGherkinUsingThePasterAddin()"));
        }
    }
}