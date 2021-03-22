using Reloadly.Airtime.Dto.Response;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Internal.Filter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public interface IPromotionOperations
    {
        Task<IList<Promotion>> GetByCountryCodeAsync(string countryCode);
        Task<Promotion> GetByIdAsync(long promotionId);
        Task<IList<Promotion>> GetByOperatorIdAsync(long operatorId);
        Task<Page<Promotion>> ListAsync(QueryFilter? filter = null);
    }
}