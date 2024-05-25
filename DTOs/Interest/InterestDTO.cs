using BankAccountAPI.DTOs.Account;

namespace BankAccountAPI.DTOs.Interest
{
    public class InterestDTO
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public AccountDTO Account { get; set; }
    }
}
