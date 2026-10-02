
using backend.DTOs.Health;
using backend.Services.HealthService;
using backend.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController(IHealthRepository healthService, IUserRepository userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var healths = await healthService.GetAllAsync();
            return Ok(healths);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var health = await healthService.GetByIdAsync(id);
            if (health == null) return NotFound();

            return Ok(health);
        }

        [HttpGet("user/{clerkId}")]
        public async Task<IActionResult> GetByClerkId([FromRoute] string clerkId)
        {
            var records = await healthService.GetHealthByClerkIdAsync(clerkId);
            return Ok(records);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHealthDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var internalUserId = await userService.GetInternalIdByClerkIdAsync(createDto.ClerkId);
            if (internalUserId == null)
            {
                return BadRequest("User does not exist in the database. Please sync the user first.");
            }

            var createdHealth = await healthService.CreateAsync(createDto, internalUserId.Value);
            return CreatedAtAction(nameof(GetById), new { id = createdHealth.Id }, createdHealth);
        }

        [HttpPut("{healthId:int}")]
        public async Task<IActionResult> Update([FromRoute] int healthId, [FromBody] UpdateHealthDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await healthService.UpdateAsync(healthId, updateDto);
            if (updated == null) return NotFound("Health record not found.");

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await healthService.DeleteAsync(id);
            if (deleted == null) return NotFound();

            return Ok(new { message = $"Health record {deleted.Id} was deleted successfully." });
        }
    }
}