using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading.Tasks;
using BankAccountOpenApiV1Sdk.Client;

namespace BankAccount.WebApi.OpenAPI.SmokeTest
{
    [TestFixture]
    public class BankServiceV1SmokeTest
    {
        private BankAccountOpenApiV1Sdk.Client.BankAccountOpenApiSdk _bankingClient;      

        [SetUp]
        public void Setup()
        {
            // Create a http cliente
            Console.WriteLine("----> Client initialized !!!");

            HttpClient httpClient = new HttpClient();            
            _bankingClient = new BankAccountOpenApiV1Sdk.Client.BankAccountOpenApiSdk("https://localhost:8443/", httpClient);

        }

        [Test]
        public async Task Test_ApiTestMethod()
        {
            // Arrange        
            // Act
            var customerResponse = await _bankingClient.TestAsync();

            //Assert & Print
            
            Assert.That(customerResponse is not null);
            //Assert.That(Equals(1, customerResponse?.CustomerId));
            Console.WriteLine($"Test response: {customerResponse ?? string.Empty}");
            
        }

        [Test]
        public async Task Test_GetAllCustomers()
        {
            // Arrange
            var customerId = 2;

            // Act
            var customerResponse = await _bankingClient.ListAllAsync();

            ////Assert & Print
            Assert.That(customerResponse is not null);
            Assert.That(customerResponse?.Any() ?? false);
            foreach (var customer in customerResponse)
            {
                Console.WriteLine($"CustomerId: {customer?.CustomerId}");
                Console.WriteLine($"FirstName: {customer?.FirstName}");
                Console.WriteLine($"LastName: {customer?.LastName}");
                Console.WriteLine($"Email: {customer?.Email}");
                Console.WriteLine($"PhoneNumber: {customer?.PhoneNumber}");
                Console.WriteLine($"DateOfBirth: {customer?.DateOfBirth}");
            }
        }

        [Test]
        public async Task Test_GetCustomerDetails()
        {
            // Arrange
            var customerId = 1;

            // Act
            var customerResponse = await _bankingClient.GetAsync(customerId);

            // Assert & Print
            Assert.That(customerResponse is not null);
            Assert.That(Equals( 1, customerResponse?.CustomerId));
            Console.WriteLine($"CustomerId: {customerResponse?.CustomerId}");
            Console.WriteLine($"FirstName: {customerResponse?.FirstName}");
            Console.WriteLine($"LastName: {customerResponse?.LastName}");
            Console.WriteLine($"Email: {customerResponse?.Email}");
            Console.WriteLine($"PhoneNumber: {customerResponse?.PhoneNumber}");
            Console.WriteLine($"DateOfBirth: {customerResponse?.DateOfBirth}");
        }
        
        [Test]
        public async Task Test_UpdateCustomerDetails()
        {
            // Arrange
            var customerId = 2;

            // Act //TODO
            var customer = await _bankingClient.GetAsync(customerId);

            string newEmail = "new@email.email";
            customer.Email = newEmail;

            var customerResponse = await _bankingClient.UpdateAsync(customer.CustomerId, customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber);

            // Assert & Print
            Assert.That(customerResponse is not null);
            Assert.That(Equals(2, customerResponse?.CustomerId));
            Assert.That(Equals(newEmail, customerResponse?.Email));
            Console.WriteLine($"CustomerId: {customerResponse?.CustomerId}");
            Console.WriteLine($"FirstName: {customerResponse?.FirstName}");
            Console.WriteLine($"LastName: {customerResponse?.LastName}");
            Console.WriteLine($"Email: {customerResponse?.Email}");
            Console.WriteLine($"PhoneNumber: {customerResponse?.PhoneNumber}");
            Console.WriteLine($"DateOfBirth: {customerResponse?.DateOfBirth}");
        }

    }
}
