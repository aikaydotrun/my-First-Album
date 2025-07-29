using Microsoft.AspNetCore.Mvc;
using TheHealthCare.Models.DTOs;
using TheHealthCare.Services;

namespace TheHealthCare.Controllers
{
    public class patientsController : Controller
    {
        private readonly patientService _service;

        public patientsController(patientService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] patientsDto dto)
        {
            var result = await _service.CreatePatient(dto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _service.GetAllPatients();
            return Ok(patients);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _service.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }
            return Ok(patient);
        }
        [HttpPut("int{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] patientsDto dto)
        {
            var result = await _service.UpdatePatient(id, dto);
            if (result == "Patient not found")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("int{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _service.DeletePatient(id);
            if (result == "Patient not found")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
