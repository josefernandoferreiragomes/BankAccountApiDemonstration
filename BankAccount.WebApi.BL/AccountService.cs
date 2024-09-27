using BankAccount.WebAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebApi.BL
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        // Create a new account for a customer
        public async Task<Account> CreateAccountAsync(int customerId, string accountType, string currency)
        {
            // Ensure customer exists
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            var newAccount = new Account
            {
                CustomerId = customerId,
                AccountType = accountType,
                Balance = 0,  // New accounts typically start with a balance of 0
                Currency = currency
            };

            await _accountRepository.AddAccountAsync(newAccount);
            return newAccount;
        }

        // Get account details by account ID
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }
            return account;
        }

        // Get all accounts for a customer
        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            return await _accountRepository.GetAccountsByCustomerIdAsync(customerId);
        }

        // Update account (e.g., close account or update type)
        public async Task UpdateAccountAsync(int accountId, string accountType)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            // Update the account type
            account.AccountType = accountType;

            await _accountRepository.UpdateAccountAsync(account);
        }

        // Delete an account (e.g., close an account)
        public async Task DeleteAccountAsync(int accountId)
        {
            await _accountRepository.DeleteAccountAsync(accountId);
        }

        // Get account balance
        public async Task<decimal> GetAccountBalanceAsync(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }
            return account.Balance;
        }
    }

}
