
using Microsoft.AspNetCore.Mvc;
using backend.Services.UserService;
using backend.DTOs.User;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserRepository userService) : ControllerBase
    {
        [HttpPost("sync")]
        public async Task<IActionResult> SyncUser([FromBody] SyncUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userService.SyncUserAsync(dto);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{clerkId}")]
        public async Task<IActionResult> GetUser(string clerkId)
        {
            var user = await userService.GetByClerkIdAsync(clerkId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}