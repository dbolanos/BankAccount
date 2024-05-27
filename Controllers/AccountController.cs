using BankAccountAPI.DTOs.Account;
using BankAccountAPI.Exceptions.Account;
using BankAccountAPI.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext context;
        private readonly IAccountService accountService;
        private readonly ILogger<AccountController> logger;

        public AccountController(ApplicationDBContext context, IAccountService accountService, ILogger<AccountController> logger)
        {
            this.context = context;
            this.accountService = accountService;
            this.logger = logger;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateAccount(CreateAccountDTO createAccountDTO)
        {
            var existCustomer = await context.Customers.AnyAsync(customerDB => customerDB.Id == createAccountDTO.CustomerId);
            if (!existCustomer)
            {
                return BadRequest("Customer not found");
            }
            var accountDTO = await accountService.CreateAccountForUser(createAccountDTO);
            return CreatedAtRoute("GetAccountById", new { id = accountDTO.Id }, accountDTO);
        }

        [HttpGet("{id:int}", Name = "GetAccountById")]
        public async Task<ActionResult> GetAccountById(int id)
        {
            try
            {
                var accountWithTransactionDTO = await accountService.GetAccountById(id);
                return Ok(accountWithTransactionDTO);
            }
            catch (AccountNotFoundException ex)
            {
                logger.LogError($" Cuenta no encontrada, id: {id}");
                return StatusCode(StatusCodes.Status404NotFound, new {error = $" Cuenta no encontrada, id: {id}" });
                
            }
            catch (InsufficientBalanceException ex)
            {
                logger.LogError($"Fondos insuficiente");
                return StatusCode(StatusCodes.Status409Conflict, new { error = $"Fondos insuficiente" });

            }
            catch (Exception ex)
            {
                logger.LogError($"Error en [AccountController] - GetAccountById, mensaje: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error" });

            }


        }
    }
}
