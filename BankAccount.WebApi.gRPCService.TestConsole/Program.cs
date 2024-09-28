// See https://aka.ms/new-console-template for more information
using BankAccount.WebApi.gRPCService;
using Grpc.Net.Client;
using System.Threading.Tasks;
using BankAccount.GrpcGreeterClient;

Console.WriteLine("Hello, World! ... waiting for service to start...");

Task.Delay(1000).Wait();

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:32779");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);

var bankingClient = new BankService.BankServiceClient(channel);

Console.WriteLine("");

// 1. Call GetCustomer and print results
Console.WriteLine("Fetching customer details...");
var customerRequest = new CustomerRequest { CustomerId = 1 };
var customerResponse = await bankingClient.GetCustomerAsync(customerRequest);
PrintCustomerDetails(customerResponse);

// 2. Call GetAccount and print results
Console.WriteLine("\nFetching account details...");
var accountRequest = new AccountRequest { AccountId = 1 };
var accountResponse = await bankingClient.GetAccountAsync(accountRequest);
PrintAccountDetails(accountResponse);

// 3. Call GetAccountsByCustomer and print results
Console.WriteLine("\nFetching all accounts for a customer...");
var accountsRequest = new AccountsByCustomerRequest { CustomerId = 1 };
var accountsResponse = await bankingClient.GetAccountsByCustomerAsync(accountsRequest);
PrintAllAccountsDetails(accountsResponse);

// 4. Call GetTransactionHistory and print results
Console.WriteLine("\nFetching transaction history...");
var transactionRequest = new TransactionHistoryRequest { AccountId = 1 };
var transactionResponse = await bankingClient.GetTransactionHistoryAsync(transactionRequest);
PrintTransactionHistory(transactionResponse);

Console.WriteLine("\nDone. Press any key to exit.");
Console.ReadKey();
        

// Helper method to print customer details
static void PrintCustomerDetails(CustomerResponse customer)
{
    Console.WriteLine("Customer Details:");
    Console.WriteLine($"CustomerId: {customer.CustomerId}");
    Console.WriteLine($"FirstName: {customer.FirstName}");
    Console.WriteLine($"LastName: {customer.LastName}");
    Console.WriteLine($"Email: {customer.Email}");
    Console.WriteLine($"PhoneNumber: {customer.PhoneNumber}");
    Console.WriteLine($"DateOfBirth: {customer.DateOfBirth}");
}

// Helper method to print account details
static void PrintAccountDetails(AccountResponse account)
{
    Console.WriteLine("Account Details:");
    Console.WriteLine($"AccountId: {account.AccountId}");
    Console.WriteLine($"CustomerId: {account.CustomerId}");
    Console.WriteLine($"AccountType: {account.AccountType}");
    Console.WriteLine($"Currency: {account.Currency}");
    Console.WriteLine($"Balance: {account.Balance}");
}

// Helper method to print all accounts for a customer
static void PrintAllAccountsDetails(AccountsResponse accountsResponse)
{
    Console.WriteLine($"Total Accounts: {accountsResponse.Accounts.Count}");
    foreach (var account in accountsResponse.Accounts)
    {
        PrintAccountDetails(account);
        Console.WriteLine();
    }
}

// Helper method to print transaction history
static void PrintTransactionHistory(TransactionHistoryResponse transactionHistory)
{
    Console.WriteLine($"Total Transactions: {transactionHistory.Transactions.Count}");
    foreach (var transaction in transactionHistory.Transactions)
    {
        Console.WriteLine($"TransactionId: {transaction.TransactionId}");
        Console.WriteLine($"TransactionType: {transaction.TransactionType}");
        Console.WriteLine($"Amount: {transaction.Amount}");
        Console.WriteLine($"Description: {transaction.Description}");
        Console.WriteLine($"TransactionDate: {transaction.TransactionDate}");
        Console.WriteLine();
    }
}



