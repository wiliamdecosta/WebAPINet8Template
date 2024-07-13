namespace Infrastructure.Models.Responses
{
	public class PageResponse
	{
		public long Total { get; set; }
		public int Size { get; set; }
		public int TotalPage { get; set; }
		public int Current { get; set; }
	}
}
