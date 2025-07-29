namespace TheRealEstate.Models.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public AgentDto Agent { get; set; }  
    }

}
