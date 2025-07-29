
namespace zibabeachresortAPI.Models.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public required string Name { get; set; }
        public required string Department { get; set; }
        public string? designation { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public string? Role { get; set; }

        internal static async Task<bool> CheckPasswordAsync(Employee employee, string password)
        {
            throw new NotImplementedException();
        }

        internal static async Task<Employee> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
