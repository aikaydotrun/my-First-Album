namespace InventoryManagement.Entities
{
    public class products
    {
        public int Id { get; set; }
        public int ProductId { get; set; } = 0;
        public required string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantities { get; set; }
        public string? ProductDescription { get; set; }

    }
}
