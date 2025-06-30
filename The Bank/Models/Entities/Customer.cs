namespace TheBank.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
