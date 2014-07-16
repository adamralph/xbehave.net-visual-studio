using System.Text;
using FluentAssertions;
using Paster.Specs.Fakes;
using SiliconSharkLtd.Paster;
using Xbehave;

namespace Paster.Specs
{
    public class SimpleGWT
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
            "And a 'Given, When, Then' snippet of gherkin".And(() =>
            {
                var sb = new StringBuilder();
                sb.AppendLine("Given a line");
                sb.AppendLine("And a line");
                sb.AppendLine("When a line");
                sb.AppendLine("Then a line");
                clipboard = FakesLibrary.CreateShim(sb.ToString());
            });
            "When the gherkin is pasted".When(() => sut.PasteGherkin(clipboard));
            "The the first line pasted should have the code '.Given(() => {});'".Then(() => environment.LinesWritten[0].Should()
                                                                                                                       .Contain(".Given(() => {});"));
            "The the first line pasted should have the code '.And(() => {});'".Then(() => environment.LinesWritten[1].Should()
                                                                                                                     .Contain(".And(() => {});"));
            "The the second line pasted should have the code '.When(() => {});'".Then(() => environment.LinesWritten[2].Should()
                                                                                                                       .Contain(".When(() => {});"));
            "The the third line pasted should have the code '.Then(() => {});'".Then(() => environment.LinesWritten[3].Should()
                                                                                                                      .Contain(".Then(() => {});"));
        }
    }
}