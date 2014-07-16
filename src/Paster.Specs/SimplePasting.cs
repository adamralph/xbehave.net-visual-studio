using System.Linq;
using System.Text;
using FluentAssertions;
using Paster.Specs.Fakes;
using SiliconSharkLtd.Paster;
using Xbehave;

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
        public void MultiLinePaste(IClipboard clipboard,
                                   GherkinPaster sut,
                                   TestEnvironment environment)
        {
            "Given a complete system".Given(() =>
            {
                environment = FakesLibrary.CreateDefaultEnvironment();
                sut = new GherkinPaster(environment);
            });
            "And multiple lines of valid gherkin".And(() =>
            {
                var sb = new StringBuilder();
                sb.AppendLine("Given a line");
                sb.AppendLine("And a line");
                sb.AppendLine("When a line");
                sb.AppendLine("Then a line");
                clipboard = FakesLibrary.CreateShim(sb.ToString());
            });
            "When the gherkin is pasted".When(() => sut.PasteGherkin(clipboard));
            "Then multiple lines is received by the environment".Then(() => environment.LinesWritten.Count()
                                                                                       .Should()
                                                                                       .Be(4));
        }
    }
}