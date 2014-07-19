// Guids.cs
// MUST match guids.h

using System;

namespace xBehave.Paster
{
    static class GuidList
    {
        public const string guidPasterPkgString = "799565f1-457b-429a-8b06-39fb8de521fb";
        public const string guidPasterCmdSetString = "859fa95e-bb4f-4ec7-8cf4-958679b31fc4";

        public static readonly Guid guidPasterCmdSet = new Guid(guidPasterCmdSetString);
    };
}