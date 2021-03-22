using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Filter;
using Reloadly.Core.Dto.Response;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public interface ITransactionHistoryOperations
    {
        Task<TopupTransaction> GetByIdAsync(long transactionId);
        Task<Page<TopupTransaction>> ListAsync();
        Task<Page<TopupTransaction>> ListAsync(TransactionHistoryFilter filter);
    }
}