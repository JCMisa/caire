
using backend.DTOs.Diagnosis;
using backend.Models;
using backend.Services.DiagnosisService;
using backend.Services.HealthService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisController(IDiagnosisRepository diagnosisService, IHealthRepository healthService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diagnoses = await diagnosisService.GetAllAsync();
            return Ok(diagnoses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var diagnosis = await diagnosisService.GetByIdAsync(id);
            if (diagnosis == null) return NotFound();

            return Ok(diagnosis);
        }

        [HttpGet("health/{healthId:int}")]
        public async Task<IActionResult> GetByHealthId([FromRoute] int healthId)
        {
            var diagnoses = await diagnosisService.GetByHealthIdAsync(healthId);
            return Ok(diagnoses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiagnosisDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Ensure foreign key target exists
            var healthExists = await healthService.GetByIdAsync(createDto.HealthId);
            if (healthExists == null)
            {
                return BadRequest($"Health record with Id {createDto.HealthId} does not exist.");
            }

            var createdDiagnosis = await diagnosisService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDiagnosis.Id }, createdDiagnosis);
        }

        [HttpPut("{diagnosisId:int}")]
        public async Task<IActionResult> Update([FromRoute] int diagnosisId, [FromBody] UpdateDiagnosisDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await diagnosisService.UpdateAsync(diagnosisId, updateDto);
            if (updated == null) return NotFound("Diagnosis not found.");

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await diagnosisService.DeleteAsync(id);
            if (deleted == null) return NotFound();

            return Ok(new { message = $"Diagnosis with id: {deleted.Id} was deleted successfully." });
        }
    }
}