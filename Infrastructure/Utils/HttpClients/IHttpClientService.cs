using Infrastructure.Models.Responses;
using Infrastructure.Util.HttpClients;

namespace CoreModules.Requests.HttpClients
{
	public interface IHttpClientService<T>
    {
        Task<BaseResponse<T>>? SendAsync(HttpClientRequest request);
    }
}
