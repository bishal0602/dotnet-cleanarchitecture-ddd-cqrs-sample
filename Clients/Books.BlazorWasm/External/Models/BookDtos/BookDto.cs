namespace Books.BlazorWasm.External.Models.BookDtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IEnumerable<AuthorForBookDto> Authors { get; set; } = new List<AuthorForBookDto>();
    }
    public class AuthorForBookDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

}
