namespace TranslationManagement.Application.Services.FileProcessing
{
    public class TxtFileProcessor : IFileProcessor
    {
        public async Task<(string StreamContent, string StreamCustomer)> ReadContentAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            return (await reader.ReadToEndAsync(), string.Empty);
        }
    }
}
