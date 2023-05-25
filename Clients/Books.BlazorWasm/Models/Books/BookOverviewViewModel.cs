namespace Books.BlazorWasm.Models.Books
{
    public class BookOverviewViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<AuthorsForBookOverviewViewModel> Authors { get; set; }
    }

    public class AuthorsForBookOverviewViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
