namespace TheRealEstate.Models.DTOs
{
    public class PriceEstimateRequestDto
    {
        public required string Location { get; set; }
        public required string PropertyType { get; set; }
        public int SizeSqm { get; set; }
        public List<string> Features { get; set; }
        public int YearBuilt { get; set; }
    }
}
