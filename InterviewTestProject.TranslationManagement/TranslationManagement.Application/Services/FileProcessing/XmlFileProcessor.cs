using System.Xml.Linq;

namespace TranslationManagement.Application.Services.FileProcessing
{
    public class XmlFileProcessor : IFileProcessor
    {
        public async Task<(string StreamContent, string StreamCustomer)> ReadContentAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var xdoc = await XDocument.LoadAsync(reader, LoadOptions.None, new CancellationToken());
            var content = xdoc.Root?.Element("Content")?.Value;
            var customer = xdoc.Root?.Element("Customer")?.Value.Trim();
            return (content ?? string.Empty, customer ?? string.Empty);
        }
    }
}
