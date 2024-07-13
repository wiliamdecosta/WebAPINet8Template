namespace Infrastructure.Models.Requests.Filters
{
	public class FilterRequest
	{
		public string Key { get; set; }
		public string Operator { get; set; }
		public string Value { get; set; }
	}
}
