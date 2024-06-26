﻿using AutoMapper;
using BankAccountAPI.DTOs.Transaction;
using BankAccountAPI.Entities;
using BankAccountAPI.Exceptions.Account;
using Microsoft.Data.SqlClient;
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

        public async Task<TransactionSuccessDTO> TransferenceAsync(TransferenceDTO transferenceDTO)
        {
            var originAccount = await context.Accounts.FirstOrDefaultAsync(accountDB => accountDB.IsActive && accountDB.Id == transferenceDTO.FromAccountId);
            var destinationAccount = await context.Accounts.FirstOrDefaultAsync(accountDB => accountDB.IsActive && accountDB.Id == transferenceDTO.ToAccountId);

            if (originAccount == null || destinationAccount == null)
            {
                throw new AccountNotFoundException("Una o ambas cuentas no se encuntran en el sistema o estan deshabilidatadas");
            }

            if(originAccount.Balance < transferenceDTO.Amount)
            {
                throw new InsufficientBalanceException("Balance insuficiente para realizar la transferencia");
            }

            var transaction = new Transaction
            {
                AccountId = originAccount.Id,
                TransactionType = TransactionType.Transference.ToString(),
                Amount = transferenceDTO.Amount,
                ToAccountId = destinationAccount.Id,
            };

            // SQL Output Parameter
            var originAccountBalanceParam = new SqlParameter("@OriginAccountBalance", System.Data.SqlDbType.Decimal)
            {
                Direction = System.Data.ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };

            var execSP = "EXEC TransferenceBetweenAccounts @OriginAccountId, @DestinationAccountId, @Amount, @OriginAccountBalance OUTPUT";
            await context.Database.ExecuteSqlRawAsync(execSP, 
                    new SqlParameter("@OriginAccountId", transferenceDTO.FromAccountId),
                    new SqlParameter("@DestinationAccountId", transferenceDTO.ToAccountId),
                    new SqlParameter("@Amount", transferenceDTO.Amount),
                    originAccountBalanceParam
                );

            // Get Value from OutPut Parameter
            var originAccountBalanceDecimal = (decimal)originAccountBalanceParam.Value;
            var originAccountBalance = (double)originAccountBalanceDecimal;

            var transactionSuccessDTO = new TransactionSuccessDTO
            {
                Amount = transferenceDTO.Amount,
                BalanceBefore = originAccount.Balance,
                BalanceAfter = originAccountBalance,
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();


            return transactionSuccessDTO;
        }

        public async Task<TransactionSuccessDTO> WithdrawlAsync(WithdrawalDTO withdrawalDTO)
        {
            var account = await GetAccountAvailable(withdrawalDTO.AccountId);

            if (account.Balance < withdrawalDTO.Amount)
            {
                throw new InsufficientBalanceException("Balance insuficiente");
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
                throw new AccountNotFoundException("Cuenta no encontrada");
            }
            return account;
        }
    }
}
