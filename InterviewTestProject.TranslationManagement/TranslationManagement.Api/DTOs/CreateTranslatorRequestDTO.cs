using System.ComponentModel.DataAnnotations;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.DTOs
{
    public class CreateTranslatorRequestDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string HourlyRate { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(TranslatorStatus))]
        public string Status { get; set; }

        [Required]
        public string CreditCardNumber { get; set; } = null!;
    }
}
