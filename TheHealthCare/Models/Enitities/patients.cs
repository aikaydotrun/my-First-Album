namespace TheHealthCare.Models.Enitities
{
    public class patients
    {
        public int Id { get; set; }
        public required string FullName { get; set; } 
        public required string Email { get; set; }
        public required string Phone { get; set; }

        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
