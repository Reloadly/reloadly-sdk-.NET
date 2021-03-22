namespace Reloadly.Core.Internal.Net
{
    internal class Telemetry
    {
        public string Name { get; private set; }
        public string? ApiVersion { get; private set; }
        public string LibraryVersion { get; private set; }

        public Telemetry(string name, string sdkVersion, string? apiVersion)
        {
            Name = name;
            LibraryVersion = sdkVersion;
            ApiVersion = apiVersion;
        }
    }
}
