using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Repositories
{
    public interface ITranslatorRepository
    {
        Task<IEnumerable<TranslatorModel>> GetTranslators();
        Task<IEnumerable<TranslatorModel>> GetTranslatorsByName(string name);
        Task<TranslatorModel?> GetTranslator(int id);
        Task<TranslatorModel> AddTranslator(TranslatorModel translator);
        Task<TranslatorModel> UpdateTranslatorStatus(int id, TranslatorStatus newStatus);
    }
}
