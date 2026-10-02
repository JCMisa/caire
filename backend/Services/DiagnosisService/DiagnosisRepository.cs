
using backend.Data;
using backend.DTOs.Diagnosis;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.DiagnosisService
{
    public class DiagnosisRepository(ApplicationDbContext context) : IDiagnosisRepository
    {
        public async Task<Diagnosis> CreateAsync(CreateDiagnosisDto dto)
        {
            var diagnosis = new Diagnosis
            {
                HealthId = dto.HealthId,
                Advice = dto.Advice,
                Symptoms = dto.Symptoms,
                Medicines = dto.Medicines,
                DiagnosedAt = DateTime.UtcNow
            };

            await context.Diagnoses.AddAsync(diagnosis);
            await context.SaveChangesAsync();
            return diagnosis;
        }

        public async Task<Diagnosis?> DeleteAsync(int diagnosisId)
        {
            var existing = await context.Diagnoses.FirstOrDefaultAsync(u => u.Id == diagnosisId);
            if (existing == null) return null;

            context.Diagnoses.Remove(existing);
            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<List<Diagnosis>> GetAllAsync()
        {
            return await context.Diagnoses.ToListAsync();
        }

        public async Task<List<Diagnosis>> GetByHealthIdAsync(int healthId)
        {
            return await context.Diagnoses
                 .Where(d => d.HealthId == healthId)
                 .OrderByDescending(d => d.DiagnosedAt)
                 .ToListAsync();
        }

        public async Task<Diagnosis?> GetByIdAsync(int diagnosisId)
        {
            return await context.Diagnoses
                .Include(d => d.Health)
                .FirstOrDefaultAsync(u => u.Id == diagnosisId);
        }

        public async Task<Diagnosis?> UpdateAsync(int diagnosisId, UpdateDiagnosisDto diagnosisDto)
        {
            var existing = await context.Diagnoses.FirstOrDefaultAsync(u => u.Id == diagnosisId);
            if (existing == null) return null;

            existing.Symptoms = diagnosisDto.Symptoms;
            existing.Medicines = diagnosisDto.Medicines;
            existing.Advice = diagnosisDto.Advice;

            await context.SaveChangesAsync();
            return existing;
        }
    }
}