using System;
using System.Collections.Generic;

namespace Reloadly.Airtime.Dto.Response
{
    public class GeographicalRechargePlan
    {
        public string LocationCode { get; set; } = default!;
        public string LocationName { get; set; } = default!;
        public IList<decimal> FixedAmounts { get; set; } = Array.Empty<decimal>();
        public IList<decimal> LocalAmounts { get; set; } = Array.Empty<decimal>();
        public IDictionary<string, string> FixedAmountsDescriptions { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> LocalFixedAmountsDescriptions { get; set; } = new Dictionary<string, string>();
    }
}
