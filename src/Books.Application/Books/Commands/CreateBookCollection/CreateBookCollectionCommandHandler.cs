using Books.Application.Contracts.Persistence;
using Books.Domain.BookAggregate;
using Books.Application.Books.Commands.SharedLogic;
using Books.Domain.BookAggregate.Entities;
using Books.Application.Extensions;

namespace Books.Application.Books.Commands.CreateBookCollection;

public class CreateBookCollectionCommandHandler
    : IRequestHandler<CreateBookCollectionCommand, Result<IEnumerable<Book>, Error>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public CreateBookCollectionCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    public async Task<Result<IEnumerable<Book>, Error>> Handle(CreateBookCollectionCommand request, CancellationToken cancellationToken)
    {
        var books = new List<Book>();
        foreach (var bookToAdd in request.BookCollection)
        {
            var validationResult = await request.ValidateAsync(new CreateBookCollectionCommandValidator(), cancellationToken);
            if (validationResult.IsFailure)
                return validationResult.Error;

            var result = await AuthorHelpers.ValidateCreateAndGetAuthorsAsync(bookToAdd.Authors, _authorRepository);
            if (result.IsFailure)
                return result.Error;

            List<Author> authors = result.Value;

            var book = Book.CreateNew(bookToAdd.Title, bookToAdd.Description);

            authors.ForEach(a => book.AddAuthor(a));

            books.Add(book);
        }

        if (books.Count != request.BookCollection.Count)
        {
            throw new Exception($"Request Cancelled: {books.Count - request.BookCollection.Count} out of {books.Count} books could not be added");
        }

        books.ForEach(b => _bookRepository.AddBook(b));
        await _bookRepository.SaveChangesAsync();
        return books;
    }
}
