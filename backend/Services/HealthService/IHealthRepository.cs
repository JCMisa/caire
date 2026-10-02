
using backend.DTOs.Health;
using backend.Models;

namespace backend.Services.HealthService
{
    public interface IHealthRepository
    {
        Task<Health> CreateAsync(CreateHealthDto dto, int internalUserId);
        Task<Health?> UpdateAsync(int id, UpdateHealthDto dto);
        Task<Health?> DeleteAsync(int id);
        Task<List<Health>> GetAllAsync();
        Task<Health?> GetByIdAsync(int id);
        Task<List<Health>> GetHealthByClerkIdAsync(string clerkId);
    }
}