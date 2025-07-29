using Microsoft.EntityFrameworkCore;
using TheHealthCare.DATA;
using TheHealthCare.Models.DTOs;
using TheHealthCare.Models.Enitities;

namespace TheHealthCare.Services
{

    public class AppointmentService
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateAppointmentAsync(AppointmentDto dto)
        {
            var doctor = await _dbContext.doctors.FindAsync(dto.DoctorId);
            var patient = await _dbContext.patients.FindAsync(dto.PatientId);

            if (doctor == null || patient == null)
                throw new Exception("Doctor or Patient not found");

            var appointment = new Appointment
            {
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                Status = dto.Status,
                Notes = dto.Notes,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId
            };

            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();

            return "Appointment created successfully";

        }
    }


}
