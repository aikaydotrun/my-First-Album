namespace InventoryManagement.Entities
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int ProductionId { get; set; }
        public required products products { get; set; }
        public required string Action { get; set; }
        public int QuantityChanged { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    }
}
