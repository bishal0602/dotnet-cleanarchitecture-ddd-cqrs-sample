namespace Books.BlazorWasm.Models.Common
{
    public class PaginationMetadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? PreviousPageLink { get; set; }
        public string? NextPageLink { get; set; }

    }
}
