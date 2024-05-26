using BankAccountAPI.DTOs.Transaction;

namespace BankAccountAPI.Services.BankService
{
    public interface IBankService
    {
        Task<TransactionSuccessDTO> DepositAsync(DepositDTO depositDTO);
        Task<TransactionSuccessDTO> WithdrawlAsync(WithdrawalDTO withdrawalDTO);
        Task<TransactionSuccessDTO> TransferenceAsync(TransferenceDTO transferenceDTO);
    }
}
