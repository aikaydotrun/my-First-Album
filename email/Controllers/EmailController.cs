using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using SendEmailApplication.Models;
using SendEmailApplication.Services;

namespace SendEmailApplication.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailController : ControllerBase, IEmailController
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail([FromBody] EmailRequest emailRequest)
        {
            try
            {
                var addr = new MailAddress(emailRequest.Receptor);
                if (addr.Address != emailRequest.Receptor)
                {
                    return BadRequest("Invalid email address format.");
                }

                var mail = new MailMessage("akaezeikenna@gmail.com", emailRequest.Receptor)
                {
                    Subject = emailRequest.Subject,
                    Body = emailRequest.Body,
                    IsBodyHtml = true
                };
                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("akaezeikenna@gmail.com", "ygcy nhvd tasp namf"),
                    EnableSsl = true
                };
                smtp.Send(mail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}











