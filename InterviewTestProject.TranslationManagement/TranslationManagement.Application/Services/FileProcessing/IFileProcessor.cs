namespace TranslationManagement.Application.Services.FileProcessing
{
    public interface IFileProcessor
    {
        Task<(string StreamContent, string StreamCustomer)> ReadContentAsync(Stream stream);
    }
}