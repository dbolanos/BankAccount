using BankAccountAPI.DTOs.Account;

namespace BankAccountAPI.Services.AccountService
{
    public interface IAccountService
    {
        Task<AccountDTO> CreateAccountForUser(CreateAccountDTO createAccountDTO);

        Task<AccountWithTransactionDTO> GetAccountById(int id);
    }
}
