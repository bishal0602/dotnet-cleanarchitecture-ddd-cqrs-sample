using Books.Application.Common;

namespace Books.Application.Books.Queries.GetBookExport
{
    public class GetBookExportQuery : IRequest<Result<FileExportModel, Error>>
    {
    }
}
