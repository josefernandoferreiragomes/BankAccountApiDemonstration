using BankAccount.WebAPI.DAL.Repositories;
using BankAccount.WebAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebApi.BL
{
    public class TransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _transactionRepository;

        public TransactionService(IAccountRepository accountRepository, IAccountTransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        // Make a deposit or withdrawal
        public async Task<AccountTransaction> CreateTransactionAsync(int accountId, string transactionType, decimal amount, string description)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            // Business logic: Ensure sufficient balance for withdrawals
            if (transactionType == "Withdrawal" && account.Balance < amount)
            {
                throw new Exception("Insufficient balance.");
            }

            // Create a new transaction
            var transaction = new AccountTransaction
            {
                AccountId = accountId,
                TransactionType = transactionType,
                Amount = amount,
                Description = description,
                TransactionDate = DateTime.UtcNow
            };

            // Update account balance
            if (transactionType == "Deposit")
            {
                account.Balance += amount;
            }
            else if (transactionType == "Withdrawal")
            {
                account.Balance -= amount;
            }

            await _transactionRepository.AddTransactionAsync(transaction);
            await _accountRepository.UpdateAccountAsync(account);  // Save updated balance

            return transaction;
        }

        // Get transaction history for an account
        public async Task<IEnumerable<AccountTransaction>> GetTransactionHistoryAsync(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            return await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
        }
    }

}
