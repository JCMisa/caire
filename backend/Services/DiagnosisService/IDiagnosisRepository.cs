

using backend.DTOs.Diagnosis;
using backend.Models;

namespace backend.Services.DiagnosisService
{
    public interface IDiagnosisRepository
    {
        Task<Diagnosis> CreateAsync(CreateDiagnosisDto dto);
        Task<Diagnosis?> UpdateAsync(int diagnosisId, UpdateDiagnosisDto diagnosisDto);
        Task<Diagnosis?> DeleteAsync(int diagnosisId);
        Task<List<Diagnosis>> GetAllAsync();
        Task<Diagnosis?> GetByIdAsync(int diagnosisId);
        Task<List<Diagnosis>> GetByHealthIdAsync(int healthId);
    }
}