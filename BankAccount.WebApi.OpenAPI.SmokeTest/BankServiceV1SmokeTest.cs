using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading.Tasks;
using BankAccountOpenApiV1Sdk.Client;
using BankAccount.WebApi.OpenAPI.Sdk;

namespace BankAccount.WebApi.OpenAPI.SmokeTest
{
    [TestFixture]
    public class BankServiceV1SmokeTest
    {
        private BankAccountOpenApiV1Sdk.Client.BankAccountOpenApiSdk _bankingClient;

        private BankAccontAlternateOpenApiClient _bankingClientAlternate;

        [SetUp]
        public void Setup()
        {
            // Create a http cliente
            Console.WriteLine("----> Client initialized !!!");

            HttpClient httpClient = new HttpClient();            
            _bankingClient = new BankAccountOpenApiV1Sdk.Client.BankAccountOpenApiSdk("https://localhost:8443/", httpClient);            

            HttpClient httpClientAlternate = new HttpClient();
            _bankingClientAlternate = new BankAccontAlternateOpenApiClient("https://localhost:8443/", httpClientAlternate);

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
            //var customerResponse = await _bankingClient.List2Async();

            ////Assert & Print
            //foreach (var customer in customerResponse)
            //{
            //    Assert.That(customerResponse is not null);
            //    Assert.That(Equals(1, customerResponse?.CustomerId));
            //    Console.WriteLine($"CustomerId: {customerResponse?.CustomerId}");
            //    Console.WriteLine($"FirstName: {customerResponse?.FirstName}");
            //    Console.WriteLine($"LastName: {customerResponse?.LastName}");
            //    Console.WriteLine($"Email: {customerResponse?.Email}");
            //    Console.WriteLine($"PhoneNumber: {customerResponse?.PhoneNumber}");
            //    Console.WriteLine($"DateOfBirth: {customerResponse?.DateOfBirth}");
            //}
        }

        [Test]
        public async Task Test_GetCustomerDetails()
        {
            // Arrange
            var customerId = 2;

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
        public async Task Test_GetCustomerDetailsAlternate()
        {
            // Arrange
            var customerId = 1;

            // Act
            var customerResponse = await _bankingClientAlternate.GetAsync(customerId);

            // Assert & Print
            Assert.That(customerResponse is not null);
            Assert.That(Equals(1, customerResponse?.CustomerId));
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
            var customerResponse = await _bankingClient.GetAsync(customerId);

            // Assert & Print
            Assert.That(customerResponse is not null);
            Assert.That(Equals(1, customerResponse?.CustomerId));
            Console.WriteLine($"CustomerId: {customerResponse?.CustomerId}");
            Console.WriteLine($"FirstName: {customerResponse?.FirstName}");
            Console.WriteLine($"LastName: {customerResponse?.LastName}");
            Console.WriteLine($"Email: {customerResponse?.Email}");
            Console.WriteLine($"PhoneNumber: {customerResponse?.PhoneNumber}");
            Console.WriteLine($"DateOfBirth: {customerResponse?.DateOfBirth}");
        }

    }
}
