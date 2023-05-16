using Books.API.Models.BookDtos;
using Books.Domain.BookAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BookResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ObjectResult? result = context.Result as ObjectResult;
            if (result?.Value == null || result.StatusCode < 200 || result.StatusCode >= 300)
            {
                await next();
                return;
            }

            Book book = (Book)result.Value;

            result.Value = new BookDto()
            {
                Id = book.Id.Value,
                Authors = book.Authors.Select(b => new AuthorForBookDto()
                {
                    Id = b.Id.Value,
                    FirstName = b.FirstName,
                    LastName = b.LastName
                }),
                Title = book.Title,
                Description = book.Description,
            };

            await next();

            return;

        }
    }
}
