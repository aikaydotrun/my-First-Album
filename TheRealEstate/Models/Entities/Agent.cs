namespace TheRealEstate.Models.Entities
{
    public class Agent
    {
        public int Id { get; set; }
        public required string FullName { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
