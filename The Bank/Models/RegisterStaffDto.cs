namespace TheBank.Models
{
    public class RegisterStaffDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public required string Role { get; set; }
        public string? Salary { get; set; }
    }
}
