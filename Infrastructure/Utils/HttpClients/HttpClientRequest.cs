namespace Infrastructure.Util.HttpClients
{
	public enum ApiType
	{
		GET,
		POST,
		PUT,
		DELETE
	}

	public class HttpClientRequest
	{
		public ApiType ApiType { get; set; }
		public string RequestUrl { get; set; }
		public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
		public object Data { get; set; }

	}
}
