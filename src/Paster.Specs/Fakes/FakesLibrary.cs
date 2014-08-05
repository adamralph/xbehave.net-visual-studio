using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;
using FakeItEasy;
using xBehave.Paster.System;

namespace Paster.Specs.Fakes
{
    public static class FakesLibrary
    {
        public static TestEnvironment CreateDefaultEnvironment()
        {
            return new TestEnvironment();
        }

        public static EnvironmentClipboard CreateShim(string text)
        {
            return new ClipboardShim(text);
        }
    }
}
