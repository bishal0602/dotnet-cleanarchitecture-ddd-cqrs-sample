namespace Books.Application.External.Models
{
    public class BookCoverDto
    {
        public BookCoverDto(string id, byte[]? content)
        {
            Id = id;
            Content = content;
        }

        public string Id { get; set; }
        public byte[]? Content { get; set; }
    }
}
