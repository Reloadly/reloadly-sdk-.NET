using Reloadly.Airtime.Operation;
using Reloadly.Core.Internal;
using System.Threading.Tasks;

namespace Reloadly.Airtime
{
    public interface IAirtimeApi
    {
        IAccountOperations Accounts { get; }
        ICountryOperations Countries { get; }
        IDiscountOperations Discounts { get; }
        IOperatorOperations Operators { get; }
        IPromotionOperations Promotions { get; }
        IReportOperations Reports { get; }
        ITopupOperations Topups { get; }

        Task<TResponse> RefreshTokenForRequest<TResponse>(ReloadlyRequest<TResponse> request, string accessToken) where TResponse : class;
    }
}