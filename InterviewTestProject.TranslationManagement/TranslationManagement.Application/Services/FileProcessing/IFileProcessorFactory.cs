namespace TranslationManagement.Application.Services.FileProcessing
{
    public interface IFileProcessorFactory
    {
        IFileProcessor GetFileProcessor(string fileExtension);
    }
}