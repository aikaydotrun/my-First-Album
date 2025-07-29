using Microsoft.AspNetCore.Mvc;
using TheRealEstate.Models.DTOs;
using TheRealEstate.Models.Entities;
using TheRealEstate.Services;

namespace TheRealEstate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDto dto)
        {
            if (dto == null)
                return BadRequest("Property data is required.");

            var createdProperty = await _propertyService.CreatePropertyAsync(dto);
            return CreatedAtAction(nameof(GetPropertyById), new { id = createdProperty.Id }, createdProperty);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var propertiesDto = await _propertyService.GetAllPropertiesAsync();
            return Ok(propertiesDto); 
        }



        [HttpGet("Get{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        
        [HttpDelete("Get{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var deleted = await _propertyService.DeletePropertyAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
