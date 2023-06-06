using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Repositories
{
    public  interface ITranslationJobRepository
    {
        Task<IEnumerable<TranslationJobModel>> GetJobs();
        Task<TranslationJobModel?> GetJob(int id);
        Task<TranslationJobModel> AddJob(TranslationJobModel job);
        Task<TranslationJobModel> UpdateJobStatus(int id, JobStatus newStatus);
    }
}
