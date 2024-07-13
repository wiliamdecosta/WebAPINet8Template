namespace Infrastructure.Models.Requests.Filters
{

	public class SearchRequest
	{

		public SearchRequest()
		{
			Filters = new List<FilterRequest>();
			Sorts = new List<SortRequest>();
		}

		public string? Keywords { get; set; }
		public List<FilterRequest> Filters { get; set; }
		public List<SortRequest> Sorts { get; set; }
		public int Page { get; set; }
		public int Size { get; set; }

	}
}
