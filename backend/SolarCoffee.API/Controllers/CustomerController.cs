using Microsoft.AspNetCore.Mvc;
using SolarCoffee.API.Serialization;
using SolarCoffee.API.ViewModels;
using SolarCoffee.Services.Customer;
using System;
using System.Linq;

namespace SolarCoffee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, 
                                  ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet()]
        public IActionResult GetCustomers()
        {
            _logger.LogInformation("Getting customers");
            var customers = _customerService.GetAll();

            if (customers is null) return NotFound();

            var customerModels = customers.Select(c => new CustomerModel 
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PrimaryAddress = CustomerMapper.MapCustomerAddress(c.PrimaryAddress),
                CreatedOn = c.CreatedOn,
                UpdatedOn = c.UpdatedOn
            })
            .OrderByDescending(c => c.CreatedOn)
            .ToList();

            return Ok(customerModels);
        }

        [HttpPost()]
        public IActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            if (customer is null) return BadRequest();
            
            if (!ModelState.IsValid)
               return BadRequest(ModelState);
               
            _logger.LogInformation("Generating customer");

            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.Create(customerData);
            
            return Ok(newCustomer);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting a customer");

            if (id == 0) return BadRequest();

            var response = _customerService.Delete(id);

            return Ok(response);
        }

        
    }
}