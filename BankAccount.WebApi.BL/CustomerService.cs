﻿using BankAccount.WebAPI.DAL;

namespace BankAccount.WebApi.BL
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Fetch customer by ID
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }
            return customer;
        }

        // Create a new customer
        public async Task<Customer> CreateCustomerAsync(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
        {
            var newCustomer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth
            };

            await _customerRepository.AddCustomerAsync(newCustomer);
            return newCustomer;
        }

        // Update customer details
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var customerUpdate = await _customerRepository.GetCustomerByIdAsync(customer.CustomerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            // Update fields
            customerUpdate.FirstName = customer.FirstName;
            customerUpdate.LastName = customer.LastName;
            customerUpdate.Email = customer.Email;
            customerUpdate.PhoneNumber = customer.PhoneNumber;

            await _customerRepository.UpdateCustomerAsync(customerUpdate);
            return customerUpdate;
        }

        public async Task<Customer> UpdateCustomerAsync(int customerId, string firstName, string lastName, string email, string phoneNumber)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            // Update fields
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Email = email;
            customer.PhoneNumber = phoneNumber;

            await _customerRepository.UpdateCustomerAsync(customer);
            return customer;
        }


        // Delete a customer
        public async Task DeleteCustomerAsync(int customerId)
        => await _customerRepository.DeleteCustomerAsync(customerId);
        

        // Get all customers
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        => await _customerRepository.GetAllCustomersAsync();
        

        public IEnumerable<CustomerAccountCard> ListCustomerAccountCardAsync(int customerId)
        => _customerRepository.ListCustomerAccountCardAsync(customerId);
        
    }

}
