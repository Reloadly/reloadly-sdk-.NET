namespace Reloadly.Airtime.Dto.Response
{
    public class SimplifiedOperator
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
        public bool Data { get; set; }
        public bool Bundle { get; set; }
    }
}
