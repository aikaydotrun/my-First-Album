using Microsoft.EntityFrameworkCore;
using TheHealthCare.DATA;
using TheHealthCare.Models.DTOs;
using TheHealthCare.Models.Enitities;

namespace TheHealthCare.Services
{
    public class DoctorService
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> createDoctor(DoctorDto dto)
        {
            var doctor = new Doctor
            {
                FullName = dto.FullName,
                Specialty = dto.Specialty

            };
            _dbContext.doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();
            return "Doctor created successfully";
        }

        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await _dbContext.doctors.ToListAsync();
        }

        public async Task<Doctor?> GetDoctorById(int id)
        {
            return await _dbContext.doctors.FindAsync(id);
        }
    }
}
