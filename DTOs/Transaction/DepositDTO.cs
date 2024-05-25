using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.DTOs.Transaction
{
    public class DepositDTO
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public double Amount { get; set; }
    }
}
