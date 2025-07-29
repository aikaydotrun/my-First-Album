using Microsoft.AspNetCore.Mvc;
using TheRealEstate.Models;
using TheRealEstate.Models.Entities;
using TheRealEstate.Services;

namespace TheRealEstate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceEstimateController : ControllerBase
    {
        private readonly IPriceEstimateService _priceEstimateService;

        public PriceEstimateController(IPriceEstimateService priceEstimateService)
        {
            _priceEstimateService = priceEstimateService;
        }

        [HttpPost]
        public IActionResult EstimatePrice([FromBody] PriceEstimateRequest request)
        {
            if (request == null || request.SizeSqm <= 0)
            {
                return BadRequest("Invalid input data.");
            }

            var estimatedPrice = _priceEstimateService.EstimatePrice(request);
            return Ok(new { EstimatedPrice = estimatedPrice });
        }
    }
}
