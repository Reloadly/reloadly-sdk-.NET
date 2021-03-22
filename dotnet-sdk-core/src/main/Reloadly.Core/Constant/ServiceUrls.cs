using Reloadly.Core.Enums;
using System;

namespace Reloadly.Core.Constant
{
    public static class ServiceUrls
    {
        public const string Airtime = "https://topups.reloadly.com";
        public const string AirtimeSandbox = "https://topups-sandbox.reloadly.com";
        public const string AirtimeAuth = "https://auth.reloadly.com";

        public static string ByEnvironment(ReloadlyEnvironment environment)
        {
            return environment switch
            {
                ReloadlyEnvironment.Sandbox => AirtimeSandbox,
                ReloadlyEnvironment.Live => Airtime,
                _ => throw new ArgumentException(nameof(environment)),
            };
        }
    }
}
