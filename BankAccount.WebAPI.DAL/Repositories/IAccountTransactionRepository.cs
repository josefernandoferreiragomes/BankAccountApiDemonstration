using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebAPI.DAL.Repositories
{
    public interface IAccountTransactionRepository
    {
        Task<IEnumerable<AccountTransaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task AddTransactionAsync(AccountTransaction transaction);
    }

}
