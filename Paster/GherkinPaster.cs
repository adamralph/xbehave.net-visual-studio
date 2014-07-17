using System;
using System.Linq;

namespace SiliconSharkLtd.Paster
{
    public class GherkinPaster
    {
        private readonly IEnvironment _environment;


        public GherkinPaster(IEnvironment environment)
        {
            _environment = environment;
        }

        public void PasteGherkin(IClipboard source)
        {
            if (!source.ContainsText())
                return;

            var gherkinText = source.GetText()
                                    .Split(new[] {Environment.NewLine},
                                           StringSplitOptions.RemoveEmptyEntries)
                                    .Select(s => s.Trim());

            var gherkinTree = new GherkinTree(LineGenerators.CSharp,
                                              GWTIdentify);
            gherkinTree.AddLines(gherkinText);

            _environment.Paste(gherkinTree.ToString());
        }

        private static Identifier GWTIdentify(string line)
        {
            if (line.StartsWith("given",
                                StringComparison.InvariantCultureIgnoreCase))
                return Identifier.Given;
            if (line.StartsWith("when",
                                StringComparison.InvariantCultureIgnoreCase))
                return Identifier.When;
            if (line.StartsWith("then",
                                StringComparison.InvariantCultureIgnoreCase))
                return Identifier.Then;
            if (line.StartsWith("and",
                                StringComparison.InvariantCultureIgnoreCase))
                return Identifier.And;
            if (line.StartsWith("scenario",
                                StringComparison.InvariantCultureIgnoreCase))
                return Identifier.Scenario;
            return Identifier.NOP;
        }
    }
}