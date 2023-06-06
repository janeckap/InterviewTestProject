using System.ComponentModel.DataAnnotations;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.Models
{
    public class TranslationJobModel
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = null!;

        public JobStatus Status { get; set; }

        [Required]
        public string OriginalContent { get; set; } = null!;

        public string? TranslatedContent { get; set; }
        
        public decimal Price { get; set; }
    }
}