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
            _bankingClient = new BankAccountOpenApiV1Sdk.Client.BankAccountOpenApiSdk("https://localhost:32771/", httpClient);
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
