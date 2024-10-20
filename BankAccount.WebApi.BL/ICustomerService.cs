using BankAccount.WebAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebApi.BL
{
    public interface ICustomerService
    {
        public Task<Customer> GetCustomerByIdAsync(int customerId);


        // Create a new customer
        public Task<Customer> CreateCustomerAsync(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth);


        // Update customer details
        public Task UpdateCustomerAsync(int customerId, string firstName, string lastName, string email, string phoneNumber);


        // Delete a customer
        public Task DeleteCustomerAsync(int customerId);


        // Get all customers
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();

        public IEnumerable<CustomerAccountCard> ListCustomerAccountCardAsync(int customerId);
    }
}
