using TranslationManagement.Application.Result;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Services
{
    public interface ITranslatorService
    {
        Task<ApiResult<IEnumerable<TranslatorModel>>> GetTranslators();
        Task<ApiResult<IEnumerable<TranslatorModel>>> GetTranslatorsByName(string name);
        Task<ApiResult<TranslatorModel>> GetTranslator(int id);
        Task<ApiResult<TranslatorModel>> AddTranslator(TranslatorModel translator);
        Task<ApiResult<TranslatorModel>> UpdateTranslatorStatus(int id, string newStatus);
    }
}