using InventoryManagement.Data;
using InventoryManagement.DTOs;
using InventoryManagement.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionController : ControllerBase
    {
        private readonly InventoryDbContext _dbContext;

        public StockTransactionController(InventoryDbContext dbContext) 
        {
          _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] StockTransactionsDto dto)
        {
            var product = await _dbContext.products.FindAsync(dto.products.Id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            product.Quantities += dto.QuantityChanged;
            var transaction = new StockTransaction
            {
                ProductionId = dto.ProductionId,
                products = product,
                Action = dto.Action,
                QuantityChanged = dto.QuantityChanged,
                Timestamp = DateTime.UtcNow
            };
            await _dbContext.SaveChangesAsync();
            _dbContext.StockTransactions.Add(transaction);
            return Ok(transaction);

        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _dbContext.StockTransactions
                .Include(t => t.products)
                .ToListAsync();
            return Ok(transactions);
        }
    }
}
