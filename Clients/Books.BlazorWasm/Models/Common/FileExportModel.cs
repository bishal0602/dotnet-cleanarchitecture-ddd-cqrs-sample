namespace Books.BlazorWasm.Models.Common
{
    public class FileExportModel
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[]? Data { get; set; }
    }
}
