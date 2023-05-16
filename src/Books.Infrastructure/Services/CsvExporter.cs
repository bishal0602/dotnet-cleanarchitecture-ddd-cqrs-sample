using Books.Application.Contracts.Services;
using Books.Domain.BookAggregate;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Books.Infrastructure.Services
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportBooksToCsv(List<Book> books)
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.Context.RegisterClassMap<BookCsvMap>();
            csvWriter.WriteRecords(books);
            return memoryStream.ToArray();
        }
        private class BookCsvMap : ClassMap<Book>
        {
            private BookCsvMap()
            {
                AutoMap(CultureInfo.InvariantCulture);
                Map(b => b.Id.Value).Name("Id");
                Map(b => b.Authors).Convert(o => string.Join(";", o.Value.Authors.Select(a => $"{a.FirstName} {a.LastName}")));

            }
        }
    }
}
