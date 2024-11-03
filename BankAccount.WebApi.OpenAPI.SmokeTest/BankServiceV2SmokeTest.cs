using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading.Tasks;
using BankAccountOpenApiV2Sdk.Client;
using System.Net.Http;
using System.Net.Http.Json;

namespace BankAccount.WebApi.OpenAPI.SmokeTest
{
    [TestFixture]
    public class BankServiceV2SmokeTest
    {
        private BankAccountOpenApiV1Sdk.Client.BankAccountOpenApiSdk _bankingV1Client;
        private BankAccountOpenApiV2Sdk.Client.BankAccountOpenApiSdk _bankingV2Client;


        [SetUp]
        public void Setup()
        {
            // Create a http cliente
            Console.WriteLine("----> Client initialized !!!");

            HttpClient httpClient = new HttpClient();
            
            _bankingV1Client = new BankAccountOpenApiV1Sdk.Client.BankAccountOpenApiSdk("https://localhost:8443/", httpClient);
            _bankingV2Client = new BankAccountOpenApiV2Sdk.Client.BankAccountOpenApiSdk("https://localhost:8443/", httpClient);
        }

       
        [Test]
        public async Task UpdateAccount_ReturnsNoContentResult()
        {
            // Arrange
            var customerId = 1;
            var customer = await _bankingV1Client.GetAsync(customerId);
            customer.FirstName = "updated first name";

            // Act
            var customerResponse = await _bankingV2Client.UpdateAsync(customer.CustomerId, customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber);
            
            // Assert
            Assert.That(customerResponse is not null);
        }




    }
}
