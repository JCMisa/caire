using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.Diagnosis
{
    public class CreateDiagnosisDto
    {
        [Required]
        public int HealthId { get; set; }

        [Required]
        public string Advice { get; set; } = string.Empty;

        public List<string> Symptoms { get; set; } = [];
        public List<string> Medicines { get; set; } = [];
    }
}