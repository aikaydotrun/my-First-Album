using Microsoft.EntityFrameworkCore;
using TheHealthCare.DATA;
using TheHealthCare.Models.DTOs;
using TheHealthCare.Models.Enitities;

namespace TheHealthCare.Services
{
    public class patientService
    {
        private readonly ApplicationDbContext _dbContext;

        public patientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> CreatePatient(patientsDto dto)
        {
            var patient = new patients
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone
            };
            _dbContext.patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            return "patient created successfully";
        }
        public async Task<List<patients>> GetAllPatients()
        {
            return await _dbContext.patients.Include(p => p.Appointments).ToListAsync();
        }
        public async Task<patients?> GetPatientById(int id)
        {
            return await _dbContext.patients.FindAsync(id);
        }
        public async Task<patients?> GetPatientByIdAsync(int id)
        {
            return await _dbContext.patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<string> UpdatePatient(int id, patientsDto dto)
        {
            var patient = await _dbContext.patients.FindAsync(id);
            if (patient == null)
            {
                return "Patient not found";
            }
            patient.FullName = dto.FullName;
            patient.Email = dto.Email;
            patient.Phone = dto.Phone;
            _dbContext.patients.Update(patient);
            await _dbContext.SaveChangesAsync();
            return "Patient updated successfully";
        }
        public async Task<string> DeletePatient(int id)
        {
            var patient = await _dbContext.patients.FindAsync(id);
            if (patient == null)
            {
                return "Patient not found";
            }
            _dbContext.patients.Remove(patient);
            await _dbContext.SaveChangesAsync();
            return "Patient deleted successfully";
        }
    }
}
