﻿using BankAccountAPI.DTOs.Transaction;
using BankAccountAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Services.BankService
{
    public class BankService : IBankService
    {
        private readonly ApplicationDBContext context;

        public BankService(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<TransactionSuccessDTO> DepositAsync(DepositDTO depositDTO)
        {
            var account = await GetAccountAvailable(depositDTO.AccountId);

            var newBalance = account.Balance + depositDTO.Amount;

            var transactionSuccessDTO = new TransactionSuccessDTO
            {
                Amount = depositDTO.Amount,
                BalanceBefore = account.Balance,
                BalanceAfter = newBalance,
            };

            var transaction = new Transaction
            {
                AccountId = account.Id,
                TransactionType = TransactionType.Deposit.ToString(),
                Amount = depositDTO.Amount,
            };
            account.Balance = newBalance;
            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            return transactionSuccessDTO;
        }

        public Task TransferenceAsync(TransferenceDTO transferenceDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionSuccessDTO> WithdrawlAsync(WithdrawalDTO withdrawalDTO)
        {
            var account = await GetAccountAvailable(withdrawalDTO.AccountId);

            if (account.Balance < withdrawalDTO.Amount)
            {
                throw new Exception("Balance insuficiente");
            }

            var newBalance = account.Balance - withdrawalDTO.Amount;

            var transaction = new Transaction
            {
                AccountId = withdrawalDTO.AccountId,
                TransactionType = TransactionType.Withdrawal.ToString(),
                Amount = withdrawalDTO.Amount,
            };

            var withdrawalSuccessDTO = new TransactionSuccessDTO
            {
                Amount = withdrawalDTO.Amount,
                BalanceBefore = account.Balance,
                BalanceAfter = newBalance,
            };

            account.Balance = newBalance;
            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            return withdrawalSuccessDTO;
        }

        private async Task<Account> GetAccountAvailable(int accountId)
        {
            var account = await context.Accounts.FirstOrDefaultAsync(accountDB => accountDB.IsActive && accountDB.Id == accountId);
            if (account == null)
            {
                throw new Exception("Cuenta no encontrada");
            }
            return account;
        }
    }
}
