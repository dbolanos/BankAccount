using BankAccountAPI.DTOs.Customer;
using BankAccountAPI.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        [HttpPost("create")]
        public async Task<ActionResult> CreateCustomer(CreateCustomerDTO createCustomerDTO)
        {
            var customerDTO = await customerService.CreateCustomerAsync(createCustomerDTO);
            return CreatedAtRoute("GetCustomerById", new { id = customerDTO.Id }, customerDTO);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCustomer()
        {
            var customer = await customerService.GetCustomersAsync();
            return Ok(customer);
        }

        [HttpGet("{id:int}", Name = "GetCustomerById")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            var customerDTO = await customerService.GetCustomerByIdAsync(id);
            return Ok(customerDTO);
        }

    }
}
