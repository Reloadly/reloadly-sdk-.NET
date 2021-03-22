namespace Reloadly.Airtime.Dto.Response
{
    public class SimplifiedCountry
    {
        /// <summary>
        /// ISO 3166-1 alpha-2 Country code. See https://www.iso.org/obp/ui/#search
        /// </summary>
        public string IsoName { get; set; } = default!;

        /// <summary>
        /// Full country name
        /// </summary>
        public string Name { get; set; } = default!;
    }
}
