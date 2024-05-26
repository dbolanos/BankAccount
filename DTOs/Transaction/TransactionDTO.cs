using BankAccountAPI.DTOs.Account;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.DTOs.Transaction
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        [Required]
        public string TransactionType { get; set; }
        [Required]
        public double Amount { get; set; }

        public int? ToAccountId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public AccountTransferenceDTO ToAccount { get; set; }
    }
}
