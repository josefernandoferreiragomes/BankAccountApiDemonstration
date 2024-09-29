using NUnit.Framework;
using System.Threading.Tasks;
using Grpc.Net.Client;
using BankAccount.WebApi.gRPCService;
using System.Threading.Tasks;
using BankAccount.GrpcGreeterClient;

namespace BankAccount.Tests
{
    [TestFixture]
    public class BankServiceSmokeTests
    {
        private BankService.BankServiceClient _bankingClient;

        [SetUp]
        public void Setup()
        {
            // Create a channel to connect to the gRPC server
            // Replace localhost with the actual host and port of the running gRPC server
            var channel = GrpcChannel.ForAddress("https://localhost:32787");
            _bankingClient = new BankService.BankServiceClient(channel);
        }

        [Test]
        public async Task Test_GetCustomerDetails()
        {
            // Arrange
            var customerRequest = new CustomerRequest { CustomerId = 1 };

            // Act
            var customerResponse = await _bankingClient.GetCustomerAsync(customerRequest);

            // Assert & Print
            Assert.NotNull(customerResponse);
            Assert.AreEqual(1, customerResponse.CustomerId);
            Console.WriteLine($"CustomerId: {customerResponse.CustomerId}");
            Console.WriteLine($"FirstName: {customerResponse.FirstName}");
            Console.WriteLine($"LastName: {customerResponse.LastName}");
            Console.WriteLine($"Email: {customerResponse.Email}");
            Console.WriteLine($"PhoneNumber: {customerResponse.PhoneNumber}");
            Console.WriteLine($"DateOfBirth: {customerResponse.DateOfBirth}");
        }

        [Test]
        public async Task Test_GetAccountDetails()
        {
            // Arrange
            var accountRequest = new AccountRequest { AccountId = 1 };

            // Act
            var accountResponse = await _bankingClient.GetAccountAsync(accountRequest);

            // Assert & Print
            Assert.NotNull(accountResponse);
            Assert.AreEqual(1, accountResponse.AccountId);
            Console.WriteLine($"AccountId: {accountResponse.AccountId}");
            Console.WriteLine($"CustomerId: {accountResponse.CustomerId}");
            Console.WriteLine($"AccountType: {accountResponse.AccountType}");
            Console.WriteLine($"Currency: {accountResponse.Currency}");
            Console.WriteLine($"Balance: {accountResponse.Balance}");
        }

        [Test]
        public async Task Test_GetAccountsByCustomer()
        {
            // Arrange
            var accountsRequest = new AccountsByCustomerRequest { CustomerId = 1 };

            // Act
            var accountsResponse = await _bankingClient.GetAccountsByCustomerAsync(accountsRequest);

            // Assert & Print
            Assert.NotNull(accountsResponse);
            Assert.IsTrue(accountsResponse.Accounts.Count > 0);
            Console.WriteLine($"Total Accounts: {accountsResponse.Accounts.Count}");
            foreach (var account in accountsResponse.Accounts)
            {
                Console.WriteLine($"AccountId: {account.AccountId}");
                Console.WriteLine($"CustomerId: {account.CustomerId}");
                Console.WriteLine($"AccountType: {account.AccountType}");
                Console.WriteLine($"Currency: {account.Currency}");
                Console.WriteLine($"Balance: {account.Balance}");
            }
        }

        [Test]
        public async Task Test_GetTransactionHistory()
        {
            // Arrange
            var transactionRequest = new TransactionHistoryRequest { AccountId = 1 };

            // Act
            var transactionResponse = await _bankingClient.GetTransactionHistoryAsync(transactionRequest);

            // Assert & Print
            Assert.NotNull(transactionResponse);
            Assert.IsTrue(transactionResponse.Transactions.Count > 0);
            Console.WriteLine($"Total Transactions: {transactionResponse.Transactions.Count}");
            foreach (var transaction in transactionResponse.Transactions)
            {
                Console.WriteLine($"TransactionId: {transaction.TransactionId}");
                Console.WriteLine($"TransactionType: {transaction.TransactionType}");
                Console.WriteLine($"Amount: {transaction.Amount}");
                Console.WriteLine($"Description: {transaction.Description}");
                Console.WriteLine($"TransactionDate: {transaction.TransactionDate}");
            }
        }
    }
}
