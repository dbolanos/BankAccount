using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Entities
{
    public class Interest
    {
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Account Account { get; set; }
    }
}
