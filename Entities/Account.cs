using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Entities
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Range(0, double.MaxValue)]
        public double Balance { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Customer Customer { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
