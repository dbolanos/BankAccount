using BankAccountAPI.DTOs.Account;
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

        public AccountController(ApplicationDBContext context, IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
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
            var accountWithTransactionDTO = await accountService.GetAccountById(id);
            return Ok(accountWithTransactionDTO);
        }
    }
}
