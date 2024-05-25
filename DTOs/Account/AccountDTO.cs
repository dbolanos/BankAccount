using BankAccountAPI.DTOs.Transaction;

namespace BankAccountAPI.DTOs.Account
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }

        public int CustomerId { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; }

        public List<TransactionDTO> Transactions { get; set; }
    }
}
