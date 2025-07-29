using Microsoft.AspNetCore.Mvc;
using TheHealthCare.Models.DTOs;
using TheHealthCare.Services;

namespace TheHealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _service;

        public AppointmentController(AppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentDto dto)
        {
           var result = await _service.CreateAppointmentAsync(dto);
            return Ok(result);
        }
    }
}
