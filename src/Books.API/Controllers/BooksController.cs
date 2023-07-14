using AutoMapper;
using Books.API.Filters;
using Books.API.Models.ApiParameters;
using Books.API.Models.BookDtos;
using Books.Application.Books.Commands.CreateBook;
using Books.Application.Books.Queries.GetBookDetail;
using Books.Application.Books.Queries.GetBookExport;
using Books.Application.Books.Queries.GetBookList;
using Books.Application.Books.Queries.GetBookListAsStream;
using Books.Application.Common;
using Books.Domain.BookAggregate;
using Books.Shared.Errors;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Books.API.Controllers
{
    [Route("api/books")]
    public class BooksController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BooksController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetBooks")]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooks([FromQuery] GetBooksQueryParameters booksQueryParameters)
        {
            var query = new GetBookListQuery(booksQueryParameters.PageNumber, booksQueryParameters.PageSize);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
                return Problem(result.Error);

            PagedList<Book> books = result.Value;

            string? previousPageLink = books.HasPrevious ? CreateBooksListUrl("PreviousPage", booksQueryParameters) : null;
            string? nextPageLink = books.HasNext ? CreateBooksListUrl("NextPage", booksQueryParameters) : null;

            var paginationMetadata = new
            {
                totalCount = books.TotalCount,
                pageSize = books.PageSize,
                currentPage = books.CurrentPage,
                totalPages = books.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination",
                   JsonSerializer.Serialize(paginationMetadata, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        private string? CreateBooksListUrl(string urlType, GetBooksQueryParameters queryParameters)
        {
            return urlType switch
            {
                ("PreviousPage") => Url.Link("GetBooks",
                                        new
                                        {
                                            pageNumber = queryParameters.PageNumber - 1,
                                            pageSize = queryParameters.PageSize
                                        }),
                ("NextPage") => Url.Link("GetBooks",
                        new
                        {
                            pageNumber = queryParameters.PageNumber + 1,
                            pageSize = queryParameters.PageSize
                        }),
                _ => null,
            };
        }


        [HttpGet("{id}", Name = "GetBookById")]
        [TypeFilter(typeof(BookWithCoversResultFilter))]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken, [FromQuery] bool includeCovers = false)
        {
            var query = new GetBookDetailQuery(id, includeCovers);
            var result = await _mediator.Send(query, cancellationToken);


            if (result.IsFailure)
            {
                return Problem(result.Error);
            }

            (Book book, IEnumerable<Application.External.Models.BookCoverDto> bookCovers) = result.Value;
            return Ok((book, bookCovers));


        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(BookDto), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto bookToAdd)
        {
            var command = new CreateBookCommand(bookToAdd.Title,
                                                bookToAdd.Description,
                                                bookToAdd.Authors.Select(a =>
                                                    new AuthorBookCommand(a.Id, a.FirstName, a.LastName, a.Bio)).ToList());
            Result<Book, Error> result = await _mediator.Send(command);

            if (result.IsFailure)
                return Problem(result.Error);

            return CreatedAtRoute("GetBookById", new { id = result.Value.Id.Value }, _mapper.Map<BookDto>(result.Value));

        }


        [HttpGet("stream")]
        public async IAsyncEnumerable<BookDto> GetBooksStream()
        {

            var query = new GetBookListAsStreamQuery();
            var result = await _mediator.Send(query);
            if (result.IsFailure)
            {
                yield break;
            }

            IAsyncEnumerable<Book> books = result.Value;
            await foreach (var book in books)
            {
                await Task.Delay(500); // for visual effects
                yield return _mapper.Map<BookDto>(book);
            }
        }

        [HttpGet("export")]
        [Authorize]
        [ProducesResponseType(typeof(FileResult), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ExportBooks()
        {
            var query = new GetBookExportQuery();
            var result = await _mediator.Send(query);

            if (result.IsFailure)
                return Problem(result.Error);

            FileExportModel file = result.Value;
            return File(file.Data, file.ContentType, file.FileName);
        }

    }
}
