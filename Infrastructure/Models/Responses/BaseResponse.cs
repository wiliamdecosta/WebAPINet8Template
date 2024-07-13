using System.Text.Json;

namespace Infrastructure.Models.Responses
{

	public class BaseResponse<T>
	{
		public int Code { get; set; }
		public string? Message { get; set; }

		public T? Data { get; set; }

		public PageResponse? Page { get; set; }

		public Dictionary<string, List<string>>? Errors { get; set; }

		private BaseResponse() { }

		public static BaseResponseBuilder<T> Builder() => new BaseResponseBuilder<T>();

		public class BaseResponseBuilder<TData>
		{
			private BaseResponse<TData> _response = new BaseResponse<TData>();

			public BaseResponseBuilder<TData> Code(int code)
			{
				_response.Code = code;
				return this;
			}

			public BaseResponseBuilder<TData> Message(string message)
			{
				_response.Message = message;
				return this;
			}

			public BaseResponseBuilder<TData> Data(TData data)
			{
				_response.Data = data;
				return this;
			}

			public BaseResponseBuilder<TData> Page(PageResponse page)
			{
				_response.Page = page;
				return this;
			}

			public BaseResponseBuilder<TData> Errors(Dictionary<string, List<string>> errors)
			{
				_response.Errors = errors;
				return this;
			}

			public BaseResponse<TData> Build() => _response;

			public Dictionary<string, dynamic> ToJSON()
			{
				var obj = new Dictionary<string, dynamic>{
					{"code", _response.Code},
					{"message", _response.Message},
					{"data", _response.Data},
					{"page", _response.Page},
					{"errors", _response.Errors}
				};

				return obj;
			}

			public string ToJSONString()
			{
				var obj = ToJSON();
				return JsonSerializer.Serialize(obj);
			}

		}
	}
}
