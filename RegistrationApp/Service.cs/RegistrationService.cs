using Microsoft.EntityFrameworkCore;
using RegistrationApp.Data;
using RegistrationApp.Models.Dto;
using RegistrationApp.Models.Entities;
using RegistrationApp.Service.cs;

namespace RegistrationApp.Service
{
    public class RegistrationService : IRegistration
    {
        private readonly ApplicationDbContext _dbContext;

        public RegistrationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Registration> RegisterAsync(RegistrationDto dto)
        {
            var registration = new Registration
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                phone = dto.phone,
                Address = dto.Address,
                Gender = dto.Gender,
                State = dto.State,
                Nationality = dto.Nationality,
                DateOfBirth = dto.DateOfBirth,
                Occupation = dto.Occupation
            };

            _dbContext.Registrations.Add(registration);
            await _dbContext.SaveChangesAsync();
            return registration;
        }

        public async Task<List<Registration>> GetAllAsync()
        {
            return await _dbContext.Registrations.ToListAsync();
        }

        public async Task<Registration?> GetByIdAsync(int id)
        {
            return await _dbContext.Registrations.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, RegistrationDto dto)
        {
            var registration = await _dbContext.Registrations.FindAsync(id);
            if (registration == null) return false;

            registration.FirstName = dto.FirstName;
            registration.LastName = dto.LastName;
            registration.Email = dto.Email;
            registration.phone = dto.phone;
            registration.Address = dto.Address;
            registration.Gender = dto.Gender;
            registration.State = dto.State;
            registration.Nationality = dto.Nationality;
            registration.DateOfBirth = dto.DateOfBirth;
            registration.Occupation = dto.Occupation;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var registration = await _dbContext.Registrations.FindAsync(id);
            if (registration == null) return false;

            _dbContext.Registrations.Remove(registration);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> UpdateAsync(int id, Registration dto)
        {
            throw new NotImplementedException();
        }
    }
}