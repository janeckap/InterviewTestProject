using Microsoft.EntityFrameworkCore;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TranslationJobModel> TranslationJobs { get; set; }
        public DbSet<TranslatorModel> Translators { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}