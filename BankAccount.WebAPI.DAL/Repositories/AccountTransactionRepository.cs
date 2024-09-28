using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebAPI.DAL.Repositories
{
    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        private readonly BankAccountContext _context;

        public AccountTransactionRepository(BankAccountContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountTransaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.AccountTransaction
                .Where(t => t.AccountId == accountId)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(AccountTransaction transaction)
        {
            _context.AccountTransaction.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }

}
