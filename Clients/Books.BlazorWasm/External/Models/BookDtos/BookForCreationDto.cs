namespace Books.BlazorWasm.External.Models.BookDtos
{
    public class BookForCreationDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IEnumerable<AuthorForBookCreationDto> Authors { get; set; } = new List<AuthorForBookCreationDto>();
    }
    //[AuthorForBookCreationValidation]
    public class AuthorForBookCreationDto
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
    }
}
