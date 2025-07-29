namespace TheRealEstate.Models.Entities
{
    public class PriceEstimateResponse
    {
        public decimal EstimatedPrice { get; set; }
        public float Confidence { get; set; }
        public decimal LocalAverage { get; set; }
        public string PricingTrend { get; set; }
    }
}
