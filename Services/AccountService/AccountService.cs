using AutoMapper;
using BankAccountAPI.DTOs.Account;
using BankAccountAPI.Entities;
using BankAccountAPI.Exceptions.Account;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public AccountService(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<AccountDTO> CreateAccountForUser(CreateAccountDTO createAccountDTO)
        {
            var account = new Account
            {
                AccountNumber = await GenerateAccountNumberAsync(),
                CustomerId = createAccountDTO.CustomerId,
                IsActive = true,
            };

            context.Add(account);
            await context.SaveChangesAsync();

            return mapper.Map<AccountDTO>(account);
        }

        public async Task<AccountWithTransactionDTO> GetAccountById(int id)
        {
                var account = await context.Accounts.Include(accountDB => accountDB.Transactions).ThenInclude(t => t.ToAccount).FirstOrDefaultAsync(accountDB => accountDB.Id == id);
                if (account == null)
                {
                    throw new AccountNotFoundException("Cuenta no encontrada");
                }
                return mapper.Map<AccountWithTransactionDTO>(account);
            }

        private async Task<string> GenerateAccountNumberAsync()
        {
            bool existAccount = false;
            string accountNumber = "";
            do
            {
                accountNumber = GenerateAccountNumber();
                existAccount = await context.Accounts.AnyAsync(accountDB => accountDB.AccountNumber == accountNumber);

            } while (existAccount);

            return accountNumber;
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            //String Interpolation
            return $"AC-{DateTime.UtcNow:yyyyMMddHHmmss}-{random.Next(1000, 9999)}";
        }
    }
}
