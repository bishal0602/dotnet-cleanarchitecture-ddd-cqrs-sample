using Books.BlazorWasm.Models.Books;
using Books.BlazorWasm.Models.Common;

namespace Books.BlazorWasm.Contracts
{
    public interface IBookService
    {
        Task<(List<BookOverviewViewModel>, PaginationMetadata?)> GetBooksList(int pageNumber, int pageSize);

        Task<BookDetailsViewModel> GetBookByIdAsync(Guid bookId, bool includeCovers);
        Task<FileExportModel> DownloadCsv();
    }
}
