using System.Threading.Tasks;

namespace Reloadly.Core.Internal.Net
{
    public interface IReloadlyHttpClient
    {
        public Task<TResponse> SendAsync<TResponse>(ReloadlyRequest<TResponse> request)
            where TResponse : class;
    }
}
