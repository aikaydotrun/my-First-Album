namespace TheHealthCare.Models.Enitities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Specialty { get; set; } = null!;


        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();


    }
}
