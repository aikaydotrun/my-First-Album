namespace SendEmailApplication.Models
{
    public class EmailRequest
    {
        public string Receptor { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
