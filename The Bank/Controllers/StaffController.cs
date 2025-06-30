using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBank.Models.Entities;
using TheBank.Models;
using TheBank.Data;
using Microsoft.AspNetCore.Authorization;
using TheBank.AuthService;

namespace TheBank.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class StaffController : ControllerBase
    {
        private readonly StaffDbContext dbContext;
        private readonly TokenProvider tokenProvider;
        private readonly loginService loginService;

        public StaffController(StaffDbContext dbContext, TokenProvider tokenProvider, loginService loginService)
        {
            this.dbContext = dbContext;
            this.tokenProvider = tokenProvider;
            this.loginService = loginService;
        }

        public class LoginInput
        {
            public required string Email { get; set; }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginInput input)
        {
            if (string.IsNullOrEmpty(input.Email))
            {
                return BadRequest("Email is required");
            }

            var staff = await dbContext.Staff.FirstOrDefaultAsync(s => s.Email == input.Email);
            if (staff == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = tokenProvider.GenerateToken(staff.Email); // assuming this is not async

            return Ok(new { token });
        }

        [HttpGet]
        public IActionResult GetallStaff()
        {
            var AllStaff = dbContext.Staff.ToList();
            return Ok(AllStaff);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetStaff(int id)
        {
            var customer = dbContext.Staff.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult RegisterStaff(RegisterStaffDto registerStaffDto)
        {
            var RegisterStaff = new Staff()
            {
                Name = registerStaffDto.Name,
                Salary = registerStaffDto.Salary,
                Email = registerStaffDto.Email,
                Phone = registerStaffDto.Phone,
                Role = registerStaffDto.Role
            };
            dbContext.Staff.Add(RegisterStaff);
            dbContext.SaveChanges();
            return Ok(RegisterStaff);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateCustomer(int id, UpdateStaffDto updateStaffDto)
        {
            var Staff = dbContext.Staff.Find(id);
            if (Staff == null)
            {
                return NotFound();
            }
            Staff.Name = updateStaffDto.Name;
            Staff.Role = updateStaffDto.Role;
            Staff.Email = updateStaffDto.Email;
            Staff.Phone = updateStaffDto.Phone;
            dbContext.Staff.Update(Staff);
            dbContext.SaveChanges();
            return Ok(Staff);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteStaff(int id)
        {
            var Staff = dbContext.Staff.Find(id);
            if (Staff == null)
            {
                return NotFound();
            }
            dbContext.Staff.Remove(Staff);
            dbContext.SaveChanges();
            return Ok(Staff);

        }
       
    }

        
    }

