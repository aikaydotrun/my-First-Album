using RegistrationApp.Models.Dto;
using RegistrationApp.Models.Entities;

namespace RegistrationApp.Service.cs
{
    public interface IRegistration
    {
        Task<Registration> RegisterAsync(RegistrationDto dto);
        Task<List<Registration>> GetAllAsync();
        Task<Registration> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Registration dto);
        Task<bool> DeleteAsync(int id);

    }
}
