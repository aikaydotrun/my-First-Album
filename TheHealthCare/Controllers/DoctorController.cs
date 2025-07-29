using Microsoft.AspNetCore.Mvc;
using TheHealthCare.Models.DTOs;
using TheHealthCare.Services;

namespace TheHealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDto dto)
        {
            var result = await _doctorService.createDoctor(dto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();
            return Ok(doctors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _doctorService.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }
            return Ok(doctor);
        }
    }
}
