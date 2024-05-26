using BankAccountAPI.DTOs.Transaction;
using BankAccountAPI.Services.BankService;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : Controller
    {
        private readonly IBankService bankService;

        public TransactionController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpPost("withdrawal")]
        public async Task<ActionResult> makeWithdrawal(WithdrawalDTO withdrawalDTO)
        {
            var transactionSuccessDTO = await bankService.WithdrawlAsync(withdrawalDTO);
            return Ok(transactionSuccessDTO);
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> makeDeposit(DepositDTO depositDTO)
        {
            var transactionSuccessDTO = await bankService.DepositAsync(depositDTO);
            return Ok(transactionSuccessDTO);
        }

        [HttpPost("transference")]
        public async Task<ActionResult> makeTransference(TransferenceDTO transferenceDTO)
        {
            var transactionSuccessDTO = await bankService.TransferenceAsync(transferenceDTO);
            return Ok(transactionSuccessDTO);
        }
    }
}
