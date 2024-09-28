using System.Threading.Tasks;
using Grpc.Core;
using BankAccount.Grpc;  // Namespace generated from your .proto file
//using BankAccount.WebAPI.DAL;
using BankAccount.WebApi.BL;
using global::Grpc.Core;

namespace BankAccount.WebApi.gRPCService.Services
{

    public class BankServiceImpl : BankService.BankServiceBase
    {
        private readonly CustomerService _customerService;
        private readonly AccountService _accountService;
        private readonly TransactionService _transactionService;

        public BankServiceImpl(CustomerService customerService, AccountService accountService, TransactionService transactionService)
        {
            _customerService = customerService;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        // Customer Services
        public override async Task<CustomerResponse> GetCustomer(CustomerRequest request, ServerCallContext context)
        {
            var customer = await _customerService.GetCustomerByIdAsync(request.CustomerId);

            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DateOfBirth = customer.DateOfBirth.ToString("yyyy-MM-dd")
            };
        }

        public override async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, ServerCallContext context)
        {
            var customer = await _customerService.CreateCustomerAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                DateTime.Parse(request.DateOfBirth)
            );

            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DateOfBirth = customer.DateOfBirth.ToString("yyyy-MM-dd")
            };
        }

        // Account Services
        public override async Task<AccountResponse> GetAccount(AccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.GetAccountByIdAsync(request.AccountId);

            return new AccountResponse
            {
                AccountId = account.AccountId,
                CustomerId = account.CustomerId,
                AccountType = account.AccountType,
                Currency = account.Currency,
                Balance = (float)account.Balance
            };
        }

        public override async Task<AccountsResponse> GetAccountsByCustomer(AccountsByCustomerRequest request, ServerCallContext context)
        {
            var accounts = await _accountService.GetAccountsByCustomerIdAsync(request.CustomerId);

            var response = new AccountsResponse();
            foreach (var account in accounts)
            {
                response.Accounts.Add(new AccountResponse
                {
                    AccountId = account.AccountId,
                    CustomerId = account.CustomerId,
                    AccountType = account.AccountType,
                    Currency = account.Currency,
                    Balance = (float)account.Balance
                });
            }

            return response;
        }

        public override async Task<AccountResponse> CreateAccount(CreateAccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.CreateAccountAsync(request.CustomerId, request.AccountType, request.Currency);

            return new AccountResponse
            {
                AccountId = account.AccountId,
                CustomerId = account.CustomerId,
                AccountType = account.AccountType,
                Currency = account.Currency,
                Balance = (float)account.Balance
            };
        }

        // Transaction Services
        public override async Task<TransactionResponse> MakeTransaction(TransactionRequest request, ServerCallContext context)
        {
            var transaction = await _transactionService.CreateTransactionAsync(
                request.AccountId,
                request.TransactionType,
                (decimal)request.Amount,
                request.Description
            );

            return new TransactionResponse
            {
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Amount = (float)transaction.Amount,
                Description = transaction.Description,
                TransactionDate = transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }

        public override async Task<TransactionHistoryResponse> GetTransactionHistory(TransactionHistoryRequest request, ServerCallContext context)
        {
            var transactions = await _transactionService.GetTransactionHistoryAsync(request.AccountId);

            var response = new TransactionHistoryResponse();
            foreach (var transaction in transactions)
            {
                response.Transactions.Add(new TransactionResponse
                {
                    TransactionId = transaction.TransactionId,
                    TransactionType = transaction.TransactionType,
                    Amount = (float)transaction.Amount,
                    Description = transaction.Description,
                    TransactionDate = transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            return response;
        }
    }
}