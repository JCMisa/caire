
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Diagnoses")]
    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }

        public string Advice { get; set; } = string.Empty;

        // EF Core 8/9/10 automatically maps List<string> to JSON columns in SQL Server
        public List<string> Symptoms { get; set; } = [];
        public List<string> Medicines { get; set; } = [];

        public DateTime DiagnosedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key pointing to the parent Health record
        public int HealthId { get; set; }

        [ForeignKey(nameof(HealthId))]
        public Health? Health { get; set; }
    }
}