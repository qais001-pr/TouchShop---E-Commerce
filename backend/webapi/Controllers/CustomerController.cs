using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly CustomerRepo customerRepo;
        public CustomerController(CustomerRepo customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        [HttpGet("getallcustomer")]
        public async Task<IActionResult> CreateCustomer()
        {
            List<Customer> customers = new List<Customer>();
            customers = await customerRepo.GetAllCustomers();
            return Ok(new { status = 200, message = "Customer created successfully.", data = customers });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.email) ||
                string.IsNullOrEmpty(customer.FullName) || customer.image == null)
            {
                return BadRequest(new { status = 500, message = "Invalid customer data." });
            }
            await customerRepo.AddCustomer(customer);
            return Ok(new { status = 200, message = "Customer created successfully." });
        }
    }
}
