
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Healths")]
    public class Health
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Illness { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Level { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key pointing to User.Id (int)
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        // Navigation property to Diagnoses
        public ICollection<Diagnosis> Diagnoses { get; set; } = [];
    }
}