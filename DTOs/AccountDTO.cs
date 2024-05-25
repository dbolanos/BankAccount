namespace BankAccountAPI.DTOs
{
    public class AccountDTO
    {
        public string AccountNumber { get; set; }

        public int CustomerId { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; }
    }
}
