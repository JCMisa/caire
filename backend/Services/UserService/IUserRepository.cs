

using backend.DTOs.User;
using backend.Models;

namespace backend.Services.UserService
{
    public interface IUserRepository
    {
        Task<User> SyncUserAsync(SyncUserDto dto);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<User?> GetByClerkIdAsync(string clerkId);
        Task<int?> GetInternalIdByClerkIdAsync(string clerkId);
    }
}