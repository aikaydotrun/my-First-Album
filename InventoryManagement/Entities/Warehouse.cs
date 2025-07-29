namespace InventoryManagement.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public required string Location { get; set; }
        public int Capacity { get; set; }
        public ICollection<products> Products { get; set; }
    }
}
