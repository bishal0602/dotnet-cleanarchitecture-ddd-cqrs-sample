namespace Books.API.Models.BookDtos
{
    public class BookDetailDto : BookDto
    {
        public IEnumerable<BookCoverDto>? BookCovers { get; set; }
        public IEnumerable<BookReviewDto> Reviews { get; set; } = new List<BookReviewDto>();
    }
}
