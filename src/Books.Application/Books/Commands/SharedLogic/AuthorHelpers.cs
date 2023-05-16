using Books.Application.Books.Commands.CreateBook;
using Books.Application.Contracts.Persistence;
using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;

namespace Books.Application.Books.Commands.SharedLogic
{
    public class AuthorHelpers
    {
        public static async Task<Result<List<Author>, Error>> ValidateCreateAndGetAuthorsAsync(List<AuthorBookCommand> authorCommands, IAuthorRepository authorRepository)
        {
            var authors = new List<Author>();
            var providedAuthorIds = new List<AuthorId>();

            foreach (var authorCommand in authorCommands)
            {
                if (authorCommand.Id is not null)
                {
                    providedAuthorIds.Add(AuthorId.Create(authorCommand.Id.Value));
                }
                else if (authorCommand.FirstName is not null && authorCommand.LastName is not null)
                {
                    authors.Add(Author.CreateNew(authorCommand.FirstName, authorCommand.LastName, authorCommand.Bio));
                }
                else
                {
                    return new ValidationError($"Author[{authorCommands.IndexOf(authorCommand)}]", "Either Id or both FirstName and LastName must be provided for Author");
                }
            }

            var authorsFromDb = await authorRepository.GetAuthorsById(providedAuthorIds);

            if (providedAuthorIds.Count != authorsFromDb.Count)
            {
                var authorIdsAdded = authorsFromDb.Select(a => a.Id).ToList();
                var authorIdsNotAdded = providedAuthorIds.Where(a => !authorIdsAdded.Contains(a)).Select(a => a.Value).ToList();
                return new NotFoundError($"Authors with following Ids were not found: {string.Join(" , ", authorIdsNotAdded)}");
            }

            authors.AddRange(authorsFromDb);
            return authors;
        }
    }
}
