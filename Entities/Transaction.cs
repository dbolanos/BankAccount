using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public double Amount { get; set; }
        public int? ToAccountId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Account Account { get; set; }
        public Account ToAccount { get; set; }
    }

    public enum TransactionType
    {
        Withdrawal,
        Deposit,
        Transference

    }
}
