using TheRealEstate.Models.DTOs;
using TheRealEstate.Models.Entities;

namespace TheRealEstate.Services
{
    public interface IPropertyService
    {
        Task<Property> CreatePropertyAsync(CreatePropertyDto dto);
        Task<List<PropertyDto>> GetAllPropertiesAsync();
        Task<Property?> GetPropertyByIdAsync(int id);
        Task<bool> DeletePropertyAsync(int id);
    }
}
