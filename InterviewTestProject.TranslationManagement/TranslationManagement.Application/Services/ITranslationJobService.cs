using Microsoft.AspNetCore.Http;
using TranslationManagement.Application.Result;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Services
{
    public interface ITranslationJobService
    {
        Task<ApiResult<IEnumerable<TranslationJobModel>>> GetJobs();
        Task<ApiResult<TranslationJobModel>> GetJob(int id);
        Task<ApiResult<TranslationJobModel>> CreateJob(TranslationJobModel job);
        Task<ApiResult<TranslationJobModel>> CreateJobWithFile(IFormFile file, string customer);
        Task<ApiResult<TranslationJobModel>> UpdateJobStatus(int jobId, int translatorId, string newStatus);
    }
}
