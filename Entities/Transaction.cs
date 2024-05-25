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
    }
}
