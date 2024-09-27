using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebAPI.DAL
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
    }

}
