using System;

namespace Reloadly.Airtime.Dto.Response
{
    public class Discount
    {
        public double Percentage { get; set; }
        public double InternationalPercentage { get; set; }
        public double LocalPercentage { get; set; }
        public DateTime UpdatedAt { get; set; }
        public SimplifiedOperator Operator { get; set; } = default!;
    }
}
