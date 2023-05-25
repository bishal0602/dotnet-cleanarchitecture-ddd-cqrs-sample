using AutoMapper;

namespace Books.BlazorWasm.Mappings
{
    public class BookMappings : Profile
    {
        public BookMappings()
        {
            CreateMap<External.Models.BookDtos.BookDto, Models.Books.BookOverviewViewModel>();
            CreateMap<External.Models.BookDtos.AuthorForBookDto, Models.Books.AuthorsForBookOverviewViewModel>();

            CreateMap<External.Models.BookDtos.BookDetailDto, Models.Books.BookDetailsViewModel>();
            CreateMap<External.Models.BookDtos.AuthorForBookDto, Models.Books.AuthorForBookDetailsViewModel>();
            CreateMap<External.Models.BookDtos.BookReviewDto, Models.Books.ReviewForBookDetailsViewModel>();

            CreateMap<External.Models.BookDtos.BookCoverDto, Models.Books.BookCoverViewModel>();
        }
    }
}
