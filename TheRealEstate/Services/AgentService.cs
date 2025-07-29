using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TheRealEstate.Data;
using TheRealEstate.Models.DTOs;
using TheRealEstate.Models.Entities;

namespace TheRealEstate.Services
{
    public class AgentService : IAgentService
    {
        private readonly ApplicationDbContext _dbContext;

        public AgentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Agent> CreateAgentAsync(AgentCreateDto dto)
        {
            var agent = new Agent
            {
                FullName = dto.FullName,
            };

            _dbContext.Agents.Add(agent);
            await _dbContext.SaveChangesAsync();

            return agent; 
        }



        public async Task<List<Agent>> GetAllAgentsAsync()
        {
            return await _dbContext.Agents.Include(a => a.Properties).ToListAsync();
        }

        public async Task<Agent> GetAgentsByIdAsync(int id)
        {
            return await _dbContext.Agents.Include(a => a.Properties)
                                           .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> DeleteAgentAsync(int id)
        {
            var agent = await _dbContext.Agents.FindAsync(id);
            if (agent == null)
                return false;

            _dbContext.Agents.Remove(agent);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
