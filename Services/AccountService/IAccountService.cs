using BankAccountAPI.DTOs.Account;

namespace BankAccountAPI.Services.AccountService
{
    public interface IAccountService
    {
        Task<AccountDTO> CreateAccountForUser(CreateAccountDTO createAccountDTO);

        Task<AccountDTO> GetAccountById(int id);
    }
}
