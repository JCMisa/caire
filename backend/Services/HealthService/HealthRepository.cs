
using backend.Data;
using backend.DTOs.Health;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.HealthService
{
    public class HealthRepository(ApplicationDbContext context) : IHealthRepository
    {
        public async Task<Health> CreateAsync(CreateHealthDto dto, int internalUserId)
        {
            var health = new Health
            {
                Illness = dto.Illness,
                Level = dto.Level,
                UserId = internalUserId,
                CreatedAt = DateTime.UtcNow
            };

            await context.Healths.AddAsync(health);
            await context.SaveChangesAsync();
            return health;
        }

        public async Task<Health?> DeleteAsync(int id)
        {
            var existing = await context.Healths.FirstOrDefaultAsync(h => h.Id == id);
            if (existing == null) return null;

            context.Healths.Remove(existing);
            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<List<Health>> GetAllAsync()
        {
            return await context.Healths
                .Include(h => h.Diagnoses)
                .ToListAsync();
        }

        public async Task<Health?> GetByIdAsync(int id)
        {
            return await context.Healths
                .Include(h => h.Diagnoses)
                .Include(h => h.User)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<Health>> GetHealthByClerkIdAsync(string clerkId)
        {
            return await context.Healths
                .Include(h => h.Diagnoses)
                .Where(h => h.User != null && h.User.ClerkId == clerkId)
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();
        }

        public async Task<Health?> UpdateAsync(int id, UpdateHealthDto dto)
        {
            var existing = await context.Healths.FirstOrDefaultAsync(h => h.Id == id);
            if (existing == null) return null;

            existing.Illness = dto.Illness;
            existing.Level = dto.Level;

            await context.SaveChangesAsync();
            return existing;
        }
    }
}