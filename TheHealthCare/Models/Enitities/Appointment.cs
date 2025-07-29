namespace TheHealthCare.Models.Enitities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public patients Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        public string Status { get; set; } = null!;
        public string? Notes { get; set; }

    }
}
