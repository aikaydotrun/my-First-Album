using Microsoft.AspNetCore.Mvc;
using RegistrationApp.Models.Dto;
using RegistrationApp.Models.Entities;
using Services;
using IRegistrationService = Services.IRegistrationService;

namespace RegistrationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegistrationDto dto)
        {
            try
            {
                var reg = await _registrationService.Create(dto);
                return Ok(reg);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _registrationService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Registration model)
        {
            var data = await _registrationService.GetByIdAsync(id);
            if (data == null) return NotFound();

            data.FirstName = model.FirstName;
            data.LastName = model.LastName;
            data.Email = model.Email;
            data.phone = model.phone;
            data.Gender = model.Gender;
            data.Address = model.Address;
            data.DateOfBirth = model.DateOfBirth;
            data.Nationality = model.Nationality;
            data.Occupation = model.Occupation;
            data.State = model.State;

            await _registrationService.UpdateAsync(data); 

            return Ok(data);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _registrationService.DeleteAsync(id);
            if (!isDeleted) return NotFound();

            return Ok(new { message = "Deleted successfully." });
        }

    }
}
