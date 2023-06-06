namespace TranslationManagement.Application.Services.FileProcessing
{
    public class FileProcessorFactory : IFileProcessorFactory
    {
        public IFileProcessor GetFileProcessor(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".txt":
                    return new TxtFileProcessor();
                case ".xml":
                    return new XmlFileProcessor();
                default:
                    throw new NotSupportedException("Unsupported file");
            }
        }
    }
}
