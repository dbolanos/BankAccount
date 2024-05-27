using BankAccountAPI.DTOs.Customer;
using BankAccountAPI.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BankAccountAPI.Exceptions.Customer.CustomerExceptions;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<AccountController> logger;

        public CustomerController(ICustomerService customerService, ILogger<AccountController> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
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
            try
            {
                var customerDTO = await customerService.GetCustomerByIdAsync(id);
                return Ok(customerDTO);
            }
            catch (CustomerNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { error = $"Cliente no encontrado" });
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en [CustomerController] - GetAllCustomerWithAccounts, mensaje: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error" });

            }
            
        }

        [HttpGet("accounts")]
        public async Task<ActionResult> GetAllCustomerWithAccounts()
        {
            var customer = await customerService.GetCustomersWithAccountsAsync();
            return Ok(customer);

        }

    }
}
