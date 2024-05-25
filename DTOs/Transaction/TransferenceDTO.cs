using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.DTOs.Transaction
{
    public class TransferenceDTO
    {
        [Required]
        public int FromAccountId { get; set; }

        [Required]
        public int ToAccountId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public double Amount { get; set; }
    }
}
