﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebAPI.DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankAccountContext _context;

        public AccountRepository(BankAccountContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts
                .Include(a => a.AccountTransactions)  // Include transactions
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }
    }

}