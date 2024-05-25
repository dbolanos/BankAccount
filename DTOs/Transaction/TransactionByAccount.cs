using BankAccountAPI.DTOs.Account;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.DTOs.Transaction
{
    public class TransactionByAccount : TransactionDTO
    {
       
        public int AccountId { get; set; }

        public AccountDTO Account { get; set; }
        
    }
}
