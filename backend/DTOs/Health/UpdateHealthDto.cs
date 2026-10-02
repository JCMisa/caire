
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Health
{
    public class UpdateHealthDto
    {
        [Required]
        [MaxLength(150)]
        public string Illness { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Level { get; set; } = string.Empty;
    }
}