using Reloadly.Airtime.Dto.Request;
using Reloadly.Airtime.Dto.Response;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public interface ITopupOperations
    {
        Task<TopupTransaction> SendAsync(TopupRequest request);
    }
}