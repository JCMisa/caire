
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Health
{
    public class CreateHealthDto
    {
        [Required]
        [MaxLength(150)]
        public string Illness { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Level { get; set; } = string.Empty;

        [Required]
        public string ClerkId { get; set; } = string.Empty;
    }
}