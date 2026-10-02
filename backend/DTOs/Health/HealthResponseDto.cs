

namespace backend.DTOs.Health
{
    public class HealthResponseDto
    {
        public int Id { get; set; }
        public string Illness { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string ClerkId { get; set; } = string.Empty;
        public List<int> DiagnosisIds { get; set; } = [];
    }
}