using System.ComponentModel.DataAnnotations;

namespace TranslationManagement.Api.DTOs
{
    public class CreateTranslationJobRequestDTO
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string OriginalContent { get; set; }
    }
}
