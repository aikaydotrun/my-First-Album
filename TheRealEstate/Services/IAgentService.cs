using TheRealEstate.Models.DTOs;
using TheRealEstate.Models.Entities;

public interface IAgentService
{
    Task<Agent> CreateAgentAsync(AgentCreateDto dto);
    Task<List<Agent>> GetAllAgentsAsync();
    Task<Agent> GetAgentsByIdAsync(int id);
    Task<bool> DeleteAgentAsync(int id);

}
