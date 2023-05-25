namespace Books.BlazorWasm.Models.Books
{
    public class BookDetailsViewModel
    {
        public List<BookCoverViewModel>? BookCovers { get; set; }
        public List<ReviewForBookDetailsViewModel> Reviews { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AuthorForBookDetailsViewModel> Authors { get; set; }
    }
    public class ReviewForBookDetailsViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
    }
    public class AuthorForBookDetailsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
