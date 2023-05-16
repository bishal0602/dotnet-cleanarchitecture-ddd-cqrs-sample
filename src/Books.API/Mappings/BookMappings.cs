using AutoMapper;
using Books.API.Models.BookDtos;

namespace Books.API.Mappings
{
    public class BookMappings : Profile
    {
        public BookMappings()
        {
            CreateMap<Domain.BookAggregate.Book, BookDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));
            CreateMap<Domain.BookAggregate.Entities.Author, AuthorForBookDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));


            CreateMap<Domain.BookAggregate.Book, BookDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<Application.External.Models.BookCoverDto, BookCoverDto>();
            CreateMap<Domain.BookAggregate.Entities.BookReview, BookReviewDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<IEnumerable<Application.External.Models.BookCoverDto>, BookDetailDto>()
                .ForMember(dest => dest.BookCovers, opt => opt.MapFrom(src => src));

        }
    }
}
