using InventoryManagement.Entities;

namespace InventoryManagement.DTOs
{
    public class StockTransactionsDto
    {
        public int ProductionId { get; set; }
        public required products products { get; set; }
        public required string Action { get; set; }
        public int QuantityChanged { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
