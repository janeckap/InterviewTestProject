using Microsoft.EntityFrameworkCore;
using TranslationManagement.Data;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Repositories
{
    public class TranslationJobRepository : ITranslationJobRepository
    {
        private readonly AppDbContext _context;

        public TranslationJobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TranslationJobModel>> GetJobs()
        {
            return await _context.TranslationJobs.ToListAsync();
        }

        public async Task<TranslationJobModel?> GetJob(int id)
        {
            return await _context.TranslationJobs.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TranslationJobModel> AddJob(TranslationJobModel job)
        {
            _context.TranslationJobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<TranslationJobModel> UpdateJobStatus(int id, JobStatus newStatus)
        {
            var job = await _context.TranslationJobs.SingleAsync(x => x.Id == id);
            job.Status = newStatus;
            await _context.SaveChangesAsync();
            return job;
        }
    }
}
