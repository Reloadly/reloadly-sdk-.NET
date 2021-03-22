using Reloadly.Airtime.Dto;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public interface IAccountOperations
    {
        Task<AccountBalanceInfo> GetBalanceAsync();
    }
}