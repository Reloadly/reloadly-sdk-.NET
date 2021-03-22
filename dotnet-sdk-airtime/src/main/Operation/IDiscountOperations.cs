using Reloadly.Airtime.Dto.Response;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Internal.Filter;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public interface IDiscountOperations
    {
        Task<Discount> GetByOperatorIdAsync(long operatorId);
        Task<Page<Discount>> ListAsync();
        Task<Page<Discount>> ListAsync(QueryFilter filter);
    }
}