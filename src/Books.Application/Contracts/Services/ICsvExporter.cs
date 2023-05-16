using Books.Domain.BookAggregate;

namespace Books.Application.Contracts.Services
{
    public interface ICsvExporter
    {
        byte[] ExportBooksToCsv(List<Book> books);
    }
}
