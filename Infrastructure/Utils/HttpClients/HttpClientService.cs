using CoreModules.Requests.HttpClients;
using Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Infrastructure.Util.HttpClients
{
	//T adalah class response yang diharapkan
	public class HttpClientService<T> : IHttpClientService<T> where T : class
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public HttpClientService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<BaseResponse<T>>? SendAsync(HttpClientRequest request)
		{
			HttpClient client = _httpClientFactory.CreateClient("ApiClient");
			HttpRequestMessage message = new();
			message.Headers.Add("Accept", "applicaton/json");

			//add additional headers
			foreach (KeyValuePair<string, string> pair in request.Headers)
			{
				message.Headers.Add(pair.Key, pair.Value);
			}

			message.RequestUri = new Uri(request.RequestUrl);
			if (request.Data != null)
			{
				message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
			}

			HttpResponseMessage? apiResponse = null;

			switch (request.ApiType)
			{
				case ApiType.GET:
					message.Method = HttpMethod.Get;
					break;
				case ApiType.POST:
					message.Method = HttpMethod.Post;
					break;
				case ApiType.PUT:
					message.Method = HttpMethod.Put;
					break;
				case ApiType.DELETE:
					message.Method = HttpMethod.Delete;
					break;
				default:
					message.Method = HttpMethod.Get;
					break;
			}

			try
			{
				apiResponse = await client.SendAsync(message);
				switch (apiResponse.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return BaseResponse<T>.Builder()
							.Code(StatusCodes.Status404NotFound)
							.Message("Not Found")
							.Build();

					case HttpStatusCode.Forbidden:
						return BaseResponse<T>.Builder()
							.Code(StatusCodes.Status403Forbidden)
							.Message("Forbidden")
							.Build();

					case HttpStatusCode.Unauthorized:
						return BaseResponse<T>.Builder()
							.Code(StatusCodes.Status401Unauthorized)
							.Message("Unauthorized")
							.Build();

					case HttpStatusCode.InternalServerError:
						return BaseResponse<T>.Builder()
							.Code(StatusCodes.Status500InternalServerError)
							.Message("Internal Server Error")
							.Build();
					default:
						var apiContent = await apiResponse.Content.ReadAsStringAsync();
						Console.WriteLine(apiContent);
						T? apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

						return BaseResponse<T>.Builder()
							.Code(StatusCodes.Status200OK)
							.Message("Success")
							.Data(apiResponseDto)
							.Build();
				}
			}
			catch (Exception ex)
			{
				return BaseResponse<T>.Builder()
						   .Code(StatusCodes.Status404NotFound)
						   .Message(ex.Message)
						   .Build();
			}

		}
	}
}
