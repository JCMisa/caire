using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs.User;

namespace backend.Services.UserService
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {

        public async Task<User> SyncUserAsync(SyncUserDto dto)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.ClerkId == dto.ClerkId);

            if (user == null)
            {
                // Create new user in SQL Server
                user = new User
                {
                    ClerkId = dto.ClerkId,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    ImageUrl = dto.ImageUrl,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(user);
            }
            else
            {
                // Keep profile fields up-to-date
                user.Email = dto.Email;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.ImageUrl = dto.ImageUrl;
            }

            await context.SaveChangesAsync();
            return user;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    ClerkId = u.ClerkId,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ImageUrl = u.ImageUrl,
                    CreatedAt = u.CreatedAt,
                    HealthRecordsCount = u.HealthRecords.Count
                })
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<User?> GetByClerkIdAsync(string clerkId)
        {
            return await context.Users
                .Include(u => u.HealthRecords)
                .FirstOrDefaultAsync(u => u.ClerkId == clerkId);
        }

        public async Task<int?> GetInternalIdByClerkIdAsync(string clerkId)
        {
            return await context.Users
                .Where(u => u.ClerkId == clerkId)
                .Select(u => (int?)u.Id)
                .FirstOrDefaultAsync();
        }
    }
}