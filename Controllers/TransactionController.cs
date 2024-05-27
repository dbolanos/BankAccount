using BankAccountAPI.DTOs.Transaction;
using BankAccountAPI.Exceptions.Account;
using BankAccountAPI.Services.BankService;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : Controller
    {
        private readonly IBankService bankService;
        private readonly ILogger<AccountController> logger;

        public TransactionController(IBankService bankService, ILogger<AccountController> logger)
        {
            this.bankService = bankService;
            this.logger = logger;
        }

        [HttpPost("withdrawal")]
        public async Task<ActionResult> makeWithdrawal(WithdrawalDTO withdrawalDTO)
        {
            try
            {
                var transactionSuccessDTO = await bankService.WithdrawlAsync(withdrawalDTO);
                return Ok(transactionSuccessDTO);
            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { error = $" Cuenta no encontrada, id: {withdrawalDTO.AccountId}" });

            }
            catch (InsufficientBalanceException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, new { error = $"Fondos insuficiente" });

            }
            catch (Exception ex)
            {
                logger.LogError($"Error en [TransactionController] - makeWithdrawal, mensaje: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error" });

            }

        }

        [HttpPost("deposit")]
        public async Task<ActionResult> makeDeposit(DepositDTO depositDTO)
        {
            try
            {
                var transactionSuccessDTO = await bankService.DepositAsync(depositDTO);
                return Ok(transactionSuccessDTO);
            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { error = $" Cuenta no encontrada, id: {depositDTO.AccountId}" });

            }
            catch (Exception ex)
            {
                logger.LogError($"Error en [TransactionController] - makeDeposit, mensaje: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error" });

            }

        }

        [HttpPost("transference")]
        public async Task<ActionResult> makeTransference(TransferenceDTO transferenceDTO)
        {
            try
            {
                var transactionSuccessDTO = await bankService.TransferenceAsync(transferenceDTO);
                return Ok(transactionSuccessDTO);
            }
            catch (AccountNotFoundException ex)
            {        
                return StatusCode(StatusCodes.Status404NotFound, new {error = $"Cuenta no encontrada" });
                
            }
            catch (InsufficientBalanceException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, new { error = $"Fondos insuficiente" });

            }
            catch (Exception ex)
            {
                logger.LogError($"Error en [TransactionController] - makeTransference, mensaje: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error" });

            }
            
        }
    }
}
