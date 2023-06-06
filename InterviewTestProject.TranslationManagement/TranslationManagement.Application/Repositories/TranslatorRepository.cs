using Microsoft.EntityFrameworkCore;
using TranslationManagement.Data;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Repositories
{
    public class TranslatorRepository : ITranslatorRepository
    {
        private readonly AppDbContext _context;

        public TranslatorRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<TranslatorModel>> GetTranslators()
        {
            return await _context.Translators.ToListAsync();
        }

        public async Task<IEnumerable<TranslatorModel>> GetTranslatorsByName(string name)
        {
            return await _context.Translators.Where(x => x.Name == name).ToListAsync();
        }

        public async Task<TranslatorModel?> GetTranslator(int id)
        {
            return await _context.Translators.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TranslatorModel> AddTranslator(TranslatorModel translator)
        {
            _context.Translators.Add(translator);
            await _context.SaveChangesAsync();
            return translator;
        }

        public async Task<TranslatorModel> UpdateTranslatorStatus(int id, TranslatorStatus newStatus)
        {
            var translator = await _context.Translators.SingleAsync(x => x.Id == id);
            translator.Status = newStatus;
            await _context.SaveChangesAsync();
            return translator;
        }
    }
}
