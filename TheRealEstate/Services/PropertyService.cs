using Microsoft.EntityFrameworkCore;
using TheRealEstate.Data;
using TheRealEstate.Models.DTOs;
using TheRealEstate.Models.Entities;

namespace TheRealEstate.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly ApplicationDbContext _dbContext;

        public PropertyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Property> CreatePropertyAsync(CreatePropertyDto dto)
        {
            var property = new Property
            {
                Location = dto.Location,
                PropertyType = dto.PropertyType,
                SizeSqm = dto.SizeSqm,
                Features = dto.Features,
                YearBuilt = dto.YearBuilt,
                ActualPrice = dto.ActualPrice,
                AgentId = dto.AgentId,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Properties.Add(property);
            await _dbContext.SaveChangesAsync();

            return property;
        }


        public async Task<List<PropertyDto>> GetAllPropertiesAsync()
        {
            var properties = await _dbContext.Properties
                                             .Include(p => p.Agent)
                                             .ToListAsync();

            return properties.Select(property => new PropertyDto
            {
                Id = property.Id,
                Address = property.Location,
                Agent = new AgentDto
                {
                    Id = property.Agent.Id,
                    FullName = property.Agent.FullName
                }
            }).ToList();
        }




        public async Task<Property?> GetPropertyByIdAsync(int id)
        {
            return await _dbContext.Properties
                .Include(p => p.Agent)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> DeletePropertyAsync(int id)
        {
            var property = await _dbContext.Properties.FindAsync(id);
            if (property == null)
                return false;

            _dbContext.Properties.Remove(property);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
