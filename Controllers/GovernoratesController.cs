using EgyptCitiesApi.DTOs;
using EgyptCitiesApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyptCitiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernoratesController(IGovernorateService governorateService) : ControllerBase
    {
        private readonly IGovernorateService _governorateService = governorateService;

        // GET: api/Governorates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GovernorateDto>>> GetGovernorates()
        {
            var governorates = await _governorateService.GetAllGovernoratesAsync();
            return Ok(governorates);
        }

        // GET: api/Governorates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GovernorateDto>> GetGovernorate(int id)
        {
            var governorate = await _governorateService.GetGovernorateByIdAsync(id);

            if (governorate == null)
            {
                return NotFound();
            }

            return Ok(governorate);
        }

        // POST: api/Governorates
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GovernorateDto>> PostGovernorate(GovernorateDto governorateDto)
        {
            var createdGovernorate = await _governorateService.AddGovernorateAsync(governorateDto);
            return CreatedAtAction(nameof(GetGovernorate), new { id = createdGovernorate.Id }, createdGovernorate);
        }

        // PUT: api/Governorates/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutGovernorate(int id, GovernorateDto governorateDto)
        {
            if (id != governorateDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var success = await _governorateService.UpdateGovernorateAsync(id, governorateDto);

            if (!success)
            {
                return NotFound(); // Or return 500 if save failed for other reasons
            }

            return NoContent();
        }

        // DELETE: api/Governorates/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGovernorate(int id)
        {
            var success = await _governorateService.DeleteGovernorateAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
