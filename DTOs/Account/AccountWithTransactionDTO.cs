using BankAccountAPI.DTOs.Transaction;

namespace BankAccountAPI.DTOs.Account
{
    public class AccountWithTransactionDTO : AccountDTO
    {
        public List<TransactionDTO> Transactions { get; set; }
    }
}
