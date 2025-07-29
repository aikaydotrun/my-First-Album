namespace TheHealthCare.Models.Enitities
{
    public class User
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHasher { get; set; }
        public required string Role { get; set; }
       
         public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
