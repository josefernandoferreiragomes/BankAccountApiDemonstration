using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading.Tasks;
using BankAccountOpenApiSdk.Client;

namespace BankAccount.WebApi.OpenAPI.SmokeTest
{
    [TestFixture]
    public class BankServiceSmokeTests
    {
        private BankAccountOpenApiSdk.Client.BankAccountOpenApiSdk _bankingClient;

        [SetUp]
        public void Setup()
        {
            // Create a http cliente
            Console.WriteLine("----> Client initialized !!!");

            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Impvc2VmIiwic3ViIjoiam9zZWYiLCJqdGkiOiJmMWFiY2FiZCIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjIzNjE5IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNTAiLCJodHRwOi8vbG9jYWxob3N0OjUwNTgiLCJodHRwczovL2xvY2FsaG9zdDo3MjU4Il0sIm5iZiI6MTcyNDQyODMzMCwiZXhwIjoxNzMyMzc3MTMwLCJpYXQiOjE3MjQ0MjgzMzIsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.BWGCAaBLLARa0fWzU1bNvMTIf-KqdOMr_Y3gxTay3H4");
            var client = new BankAccountOpenApiSdk.Client.BankAccountOpenApiSdk("https://localhost:32773/", httpClient);
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

      
    }
}
