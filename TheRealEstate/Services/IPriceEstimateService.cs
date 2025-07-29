using TheRealEstate.Models.Entities;

namespace TheRealEstate.Services
{
    public interface IPriceEstimateService
    {
        decimal EstimatePrice(PriceEstimateRequest request);
    }
}
