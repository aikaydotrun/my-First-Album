using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBank.Data;
using TheBank.Models;
using TheBank.Models.Entities;

namespace TheBank.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext dbContext;

        public CustomerController(CustomerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetallCustomers()
        {
            var AllCustomers = dbContext.customers.ToList();
            return Ok(AllCustomers);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = dbContext.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult RegisterCustomer(RegisterCustomerDto registerCustomerDto)
        {
            var RegisterCustomer = new Customer()
            {
                Name = registerCustomerDto.Name,
                Address = registerCustomerDto.Address,
                Email = registerCustomerDto.Email,
                Phone = registerCustomerDto.Phone
            };
            dbContext.customers.Add(RegisterCustomer);
            dbContext.SaveChanges();
            return Ok(RegisterCustomer);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = dbContext.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Name = updateCustomerDto.Name;
            customer.Address = updateCustomerDto.Address;
            customer.Email = updateCustomerDto.Email;
            customer.Phone = updateCustomerDto.Phone;
            dbContext.customers.Update(customer);
            dbContext.SaveChanges();
            return Ok(customer);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = dbContext.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            dbContext.customers.Remove(customer);
            dbContext.SaveChanges();
            return Ok(customer);

        }
    }
}
