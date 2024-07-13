namespace Infrastructure.Models.Requests.Filters
{
	public class Paginated<T>
	{
		public IQueryable<T> Data { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
		public int PageSize { get; set; }
		public int PageNumber { get; set; }

	}
}
