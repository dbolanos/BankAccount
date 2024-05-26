using BankAccountAPI.DTOs.Account;

namespace BankAccountAPI.DTOs.Customer
{
    public class CustomerWithAccountsDTO : CustomerDTO
    {
        public List<AccountDTO> Accounts { get; set; }
    }
}
