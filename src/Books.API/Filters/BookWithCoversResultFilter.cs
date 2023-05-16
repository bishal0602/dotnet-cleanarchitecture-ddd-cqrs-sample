using AutoMapper;
using Books.API.Models.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BookWithCoversResultFilter : IAsyncResultFilter
    {
        private readonly IMapper _mapper;

        public BookWithCoversResultFilter(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ObjectResult? result = context.Result as ObjectResult;
            if (result?.Value == null || result.StatusCode < 200 || result.StatusCode >= 300)
            {
                await next();
                return;
            }


            var (book, bookCovers) = ((Domain.BookAggregate.Book book, IEnumerable<Application.External.Models.BookCoverDto> bookCovers))result.Value;


            BookDetailDto bookDetailDto = _mapper.Map<BookDetailDto>(book);
            _mapper.Map(bookCovers, bookDetailDto);

            result.Value = bookDetailDto;

            await next();
            return;
        }
    }
}
