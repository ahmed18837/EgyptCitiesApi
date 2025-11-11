using EgyptCitiesApi.DTOs;
using EgyptCitiesApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyptCitiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // GET: api/Cities/ByGovernorate/100 (لجلب مدن محافظة معينة)
        [HttpGet("ByGovernorate/{governorateId}")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCitiesByGovernorate(int governorateId)
        {
            var cities = await _cityService.GetCitiesByGovernorateIdAsync(governorateId);

            if (cities == null || !cities.Any())
            {
                return NotFound();
            }

            return Ok(cities);
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<CityDto>> PostCity(CityDto cityDto)
        {
            var createdCity = await _cityService.AddCityAsync(cityDto);

            if (createdCity == null)
            {
                // إذا لم يتم العثور على المحافظة المذكورة في الـ DTO
                return BadRequest("Invalid GovernorateId provided.");
            }

            return CreatedAtAction(nameof(GetCity), new { id = createdCity.Id }, createdCity);
        }

        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, CityDto cityDto)
        {
            if (id != cityDto.Id)
            {
                return BadRequest("ID mismatch between route and request body.");
            }

            var success = await _cityService.UpdateCityAsync(id, cityDto);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var success = await _cityService.DeleteCityAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
