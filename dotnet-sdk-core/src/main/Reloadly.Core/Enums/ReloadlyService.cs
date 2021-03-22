using Reloadly.Core.Constant;

namespace Reloadly.Core.Enums
{
    public class ReloadlyService
    {
        public static readonly ReloadlyService Airtime = new ReloadlyService(ServiceUrls.Airtime);
        public static readonly ReloadlyService AirtimeSandbox = new ReloadlyService(ServiceUrls.AirtimeSandbox);

        public string Url { get; set; }

        public ReloadlyService (string url)
        {
            Url = url;
        }
    }
}
