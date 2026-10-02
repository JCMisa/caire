

using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Diagnosis
{
    public class UpdateDiagnosisDto
    {
        [Required]
        public string Advice { get; set; } = string.Empty;

        public List<string> Symptoms { get; set; } = [];
        public List<string> Medicines { get; set; } = [];
    }
}