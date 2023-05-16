using Books.Application.Common;
using Books.Application.Contracts.Persistence;
using Books.Application.Contracts.Services;

namespace Books.Application.Books.Queries.GetBookExport
{
    public class GetBookExportQueryHandler : IRequestHandler<GetBookExportQuery, Result<FileExportModel, Error>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICsvExporter _csvExporter;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GetBookExportQueryHandler(IBookRepository bookRepository, ICsvExporter csvExporter, IDateTimeProvider dateTimeProvider)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _csvExporter = csvExporter ?? throw new ArgumentNullException(nameof(csvExporter));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }
        public async Task<Result<FileExportModel, Error>> Handle(GetBookExportQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBooksAsync();

            var fileData = _csvExporter.ExportBooksToCsv(books.ToList());

            var exportFile = new FileExportModel() { ContentType = "text/csv", Data = fileData, FileName = $"BookList-{_dateTimeProvider.Now.ToFileTime()}.csv" };

            return exportFile;

        }
    }
}
