using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zibabeachresortAPI.AuthService;
using zibabeachresortAPI.AuthService.zibabeachresortAPI.AuthService;
using zibabeachresortAPI.DATA;
using zibabeachresortAPI.Models;
using zibabeachresortAPI.Models.Entities;
using zibabeachresortAPI.Services;

namespace zibabeachresortAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TokenProvider _tokenProvider;
        private readonly LoginService _loginService;

        public EmployeeController(ApplicationDbContext dbContext, TokenProvider tokenProvider, LoginService loginService)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
            _loginService = loginService;
        }

        public class LoginInput
        {
            public required string Email { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInput input)
        {
            try
            {
                var token = await _loginService.HandleAsync(input.Email);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stack = ex.StackTrace });
            }
        }


        [HttpGet]
        public IActionResult GetEmployees()
        {
            var allEmployees = _dbContext.employees.ToList();
            return Ok(allEmployees);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee
            {
                Name = addEmployeeDto.Name,
                Department = addEmployeeDto.Department,
                designation = addEmployeeDto.designation,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };
            _dbContext.employees.Add(employeeEntity);
            _dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, updateEmployeeDto updateEmployeeDto)
        {
            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Department = updateEmployeeDto.Department;
            employee.designation = updateEmployeeDto.designation;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            _dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            _dbContext.employees.Remove(employee);
            _dbContext.SaveChanges();
            return Ok($"Employee with ID {id} deleted successfully.");
        }
    }
}
