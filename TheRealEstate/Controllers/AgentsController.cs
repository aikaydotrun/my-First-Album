using Microsoft.AspNetCore.Mvc;
using TheRealEstate.Models.DTOs;
using TheRealEstate.Models.Entities;
using TheRealEstate.Services;

namespace TheRealEstate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await _agentService.GetAllAgentsAsync(); 
            return Ok(agents);
        }

        
        [HttpGet("Get{id}")]
        public async Task<IActionResult> GetAgentById(int id)
        {
            var agent = await _agentService.GetAgentsByIdAsync(id);
            if (agent == null)
                return NotFound();

            return Ok(agent);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAgent([FromBody] AgentCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest("Full name is required.");

            var agent = await _agentService.CreateAgentAsync(dto);
            return CreatedAtAction(nameof(GetAgentById), new { id = agent.Id }, agent);
        }

        
        [HttpDelete("Get{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            var deleted = await _agentService.DeleteAgentAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Agent with ID {id} not found." });

            return Ok(new { message = $"Agent with ID {id} deleted successfully." });
        }
    }
}
