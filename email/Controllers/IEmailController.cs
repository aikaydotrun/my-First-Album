using Microsoft.AspNetCore.Mvc;
using SendEmailApplication.Models;

namespace SendEmailApplication.Controllers
{
    public interface IEmailController
    {
        IActionResult SendEmail([FromBody] EmailRequest emailRequest);
    }
}