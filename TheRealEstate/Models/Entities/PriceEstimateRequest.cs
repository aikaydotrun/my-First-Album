namespace TheRealEstate.Models.Entities
{
    public class PriceEstimateRequest
    {
        public required string Location { get; set; }
        public required string PropertyType { get; set; }
        public int SizeSqm { get; set; }
        public List<string> Features { get; set; }
        public int YearBuilt { get; set; }
    }
}
