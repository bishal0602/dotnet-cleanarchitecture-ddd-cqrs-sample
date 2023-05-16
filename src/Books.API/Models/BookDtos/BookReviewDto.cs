namespace Books.API.Models.BookDtos
{
    public class BookReviewDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}
