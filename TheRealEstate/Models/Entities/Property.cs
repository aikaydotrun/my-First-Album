namespace TheRealEstate.Models.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public required string Location { get; set; }
        public required string PropertyType { get; set; }
        public int SizeSqm { get; set; }
        public required string Features { get; set; }
        public int YearBuilt { get; set; }
        public decimal? ActualPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
