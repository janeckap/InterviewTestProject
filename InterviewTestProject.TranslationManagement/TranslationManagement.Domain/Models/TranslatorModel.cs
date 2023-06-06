using System.ComponentModel.DataAnnotations;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.Models
{
    public class TranslatorModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string HourlyRate { get; set; } = null!;

        public TranslatorStatus Status { get; set; }

        [Required]
        public string CreditCardNumber { get; set; } = null!;
    }
}