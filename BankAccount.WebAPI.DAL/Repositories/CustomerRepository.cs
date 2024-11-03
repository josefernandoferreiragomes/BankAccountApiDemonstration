using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebAPI.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankAccountContext _context;

        public CustomerRepository(BankAccountContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                var customer = await _context.Customer
                    .Include(c => c.Accounts)  // Include accounts in the result
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customer.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<CustomerAccountCard> ListCustomerAccountCardAsync(int customerId)
           => _context.Database.SqlQueryRaw<CustomerAccountCard>("dbo.ListCustomerAccountCardAsync", new { customerId = customerId })?.ToList();
      
    }

}
