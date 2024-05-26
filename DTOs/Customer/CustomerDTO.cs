using BankAccountAPI.DTOs.Account;

namespace BankAccountAPI.DTOs.Customer
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string email { get; set; }

        public DateTime CreatedAt { get; set; }

        
    }
}
