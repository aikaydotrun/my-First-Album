using TheRealEstate.Models.Entities;

namespace TheRealEstate.Services
{
    public class PriceEstimateService : IPriceEstimateService
    {
        public decimal EstimatePrice(PriceEstimateRequest request)
        {
            decimal basePricePerSqm = GetBasePricePerSqm(request.Location, request.PropertyType);
            decimal sizeComponent = basePricePerSqm * request.SizeSqm;

            decimal featureBoost = request.Features.Count * 250_000; 
            decimal agePenalty = CalculateAgePenalty(request.YearBuilt);

            decimal estimatedPrice = sizeComponent + featureBoost - agePenalty;

            return Math.Max(estimatedPrice, 1_000_000); 
        }

        private decimal GetBasePricePerSqm(string location, string propertyType)
        {
            decimal price = location.ToLower() switch
            {
                "lekki" => 400_000,
                "ajah" => 300_000,
                "ikoyi" => 800_000,
                "gwarinpa" => 250_000,
                _ => 150_000
            };

            if (propertyType.ToLower().Contains("duplex")) price *= 1.3M;
            if (propertyType.ToLower().Contains("bungalow")) price *= 1.1M;

            return price;
        }

        private decimal CalculateAgePenalty(int yearBuilt)
        {
            int age = DateTime.Now.Year - yearBuilt;
            return age > 5 ? age * 50_000 : 0;
        }
    }
}
