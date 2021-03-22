using Reloadly.Airtime.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public interface ICountryOperations
    {
        Task<Country> GetByCodeAsync(string countryCode);
        Task<IList<Country>> ListAsync();
    }
}