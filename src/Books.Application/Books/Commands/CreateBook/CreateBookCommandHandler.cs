using Books.Application.Books.Commands.SharedLogic;
using Books.Application.Contracts.Persistence;
using Books.Application.Extensions;
using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.Entities;
using FluentValidation;


namespace Books.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<Book, Error>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }
        public async Task<Result<Book, Error>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Result<bool, ValidationError> validationResult = await request.ValidateAsync(new CreateBookCommandValidator(), cancellationToken);
            if (validationResult.IsFailure)
                return validationResult.Error;

            var result = await AuthorHelpers.ValidateCreateAndGetAuthorsAsync(request.Authors, _authorRepository);
            if (result.IsFailure)
                return result.Error;

            List<Author> authors = result.Value;
            var book = Book.CreateNew(request.Title, request.Description);

            authors.ForEach(a => book.AddAuthor(a));

            _bookRepository.AddBook(book);

            await _bookRepository.SaveChangesAsync();

            return book;
        }
    }
}



