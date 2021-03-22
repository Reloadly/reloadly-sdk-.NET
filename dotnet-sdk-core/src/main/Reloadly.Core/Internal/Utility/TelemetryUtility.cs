using Newtonsoft.Json;
using Reloadly.Core.Internal.Net;
using System.Collections.Generic;

namespace Reloadly.Core.Internal.Utility
{
    internal static class TelemetryUtility
    {
        private const string ENV_KEY = "env";
        private const string NAME_KEY = "name";
        private const string VERSION_KEY = "api-version";
        private const string LIBRARY_VERSION_KEY = "reloadly-sdk-dotnet";

        internal static Telemetry Create(string? apiVersion)
        {
            var sdkVersion = typeof(TelemetryUtility).Assembly.GetName().Version.ToString();
            return new Telemetry("reloadly-sdk-dotnet", sdkVersion, apiVersion);
        }

        internal static string HttpHeaderValue(this Telemetry telemetry)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(telemetry.Name)) values[NAME_KEY] = telemetry.Name;
            if (!string.IsNullOrWhiteSpace(telemetry.ApiVersion)) values[VERSION_KEY] = telemetry.ApiVersion;

            var env = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(telemetry.LibraryVersion)) env[LIBRARY_VERSION_KEY] = telemetry.LibraryVersion;
            values[ENV_KEY] = env;

            string json = JsonConvert.SerializeObject(values);
            return Base64.Encode(json);
        }
    }
}
