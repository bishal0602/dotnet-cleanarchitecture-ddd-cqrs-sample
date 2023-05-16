using Books.Domain.BookAggregate;
using Microsoft.AspNetCore.Mvc;
using Books.API.Helpers;
using AutoMapper;
using MediatR;
using Books.Application.Books.Queries.GetBookCollection;
using Books.Application.Books.Commands.CreateBookCollection;
using Books.Application.Books.Commands.CreateBook;
using Books.API.Models.BookDtos;
using Microsoft.AspNetCore.Authorization;

namespace Books.API.Controllers
{
    [Route("api/bookscollection")]
    public class BookCollectionController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BookCollectionController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{bookIds}", Name = "GetBookCollection")]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookCollection([ModelBinder(typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> bookIds, CancellationToken cancellationToken)
        {
            var query = new GetBookCollectionQuery(bookIds);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
                return Problem(result.Error);

            IEnumerable<Book> books = result.Value;

            IEnumerable<BookDto> booksToReturn = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(booksToReturn);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBookCollection([FromBody] IEnumerable<BookForCreationDto> booksToAdd)
        {

            var command = new CreateBookCollectionCommand(
                booksToAdd.Select(b => new CreateBookCommand(b.Title,
                                                        b.Description,
                                                        b.Authors.Select(a => new AuthorBookCommand(a.Id,
                                                                                  a.FirstName,
                                                                                  a.LastName,
                                                                                  a.Bio)).ToList())).ToList());
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return Problem(result.Error);

            IEnumerable<Book> bookCollection = result.Value;
            var bookIds = string.Join(",", bookCollection.Select(b => b.Id.Value));
            return CreatedAtRoute(
                "GetBookCollection",
                new { bookIds },
                _mapper.Map<IEnumerable<BookDto>>(bookCollection));
        }
    }
}
