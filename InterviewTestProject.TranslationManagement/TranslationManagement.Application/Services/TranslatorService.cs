using TranslationManagement.Application.Repositories;
using TranslationManagement.Application.Result;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly ITranslatorRepository _translatorRepository;

        public TranslatorService(ITranslatorRepository translatorRepository)
        {
            _translatorRepository = translatorRepository;
        }

        public async Task<ApiResult<IEnumerable<TranslatorModel>>> GetTranslators()
        {
            var result = await _translatorRepository.GetTranslators();
            return ApiResult<IEnumerable<TranslatorModel>>.SuccessResult(result);
        }

        public async Task<ApiResult<IEnumerable<TranslatorModel>>> GetTranslatorsByName(string name)
        {
            var result = await _translatorRepository.GetTranslatorsByName(name);
            return ApiResult<IEnumerable<TranslatorModel>>.SuccessResult(result);
        }

        public async Task<ApiResult<TranslatorModel>> GetTranslator(int id)
        {
            var result = await _translatorRepository.GetTranslator(id);
            if (result == null)
            {
                return ApiResult<TranslatorModel>.NotFoundResult();
            }
            else
            {
                return ApiResult<TranslatorModel>.SuccessResult(result);
            }
        }

        public async Task<ApiResult<TranslatorModel>> AddTranslator(TranslatorModel translator)
        {
            var result = await _translatorRepository.AddTranslator(translator);
            return ApiResult<TranslatorModel>.SuccessResult(result);
        }

        public async Task<ApiResult<TranslatorModel>> UpdateTranslatorStatus(int id, string newStatus)
        {
            if (await _translatorRepository.GetTranslator(id) == null)
            {
                return ApiResult<TranslatorModel>.NotFoundResult();
            }

            if (!Enum.TryParse(typeof(TranslatorStatus), newStatus, true, out var status))
            {
                return ApiResult<TranslatorModel>.BadRequestResult();
            }

            var result = await _translatorRepository.UpdateTranslatorStatus(id, (TranslatorStatus) status);
            return ApiResult<TranslatorModel>.SuccessResult(result);
        }

    }
}
