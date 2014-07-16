using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiliconSharkLtd.Paster
{
    internal enum Identifier
    {
        Given,
        When,
        Then,
        And,
        NOP
    }

    public class GherkinPaster
    {
        private readonly IEnvironment _environment;

        private static readonly Dictionary<Identifier, Func<string, string>> Formatters = new Dictionary<Identifier, Func<string, string>>
        {
            {
                Identifier.Given, line => String.Format(@"""{0}"".Given(() => {{}});",
                                                        line)
            },
            {
                Identifier.When, line => String.Format(@"""{0}"".When(() => {{}});",
                                                       line)
            },
            {
                Identifier.Then, line => String.Format(@"""{0}"".Then(() => {{}});",
                                                       line)
            },
            {
                Identifier.And, line => String.Format(@"""{0}"".And(() => {{}});",
                                                      line)
            },
            {Identifier.NOP, line => String.Empty},
        };

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
            var sb = new StringBuilder();

            foreach (var line in gherkinText)
            {
                var id = GWTIdentify(line);
                var code = Formatters[id](line);
                sb.AppendLine(code);
            }

            _environment.Paste(sb.ToString());
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
            return Identifier.NOP;
        }
    }
}