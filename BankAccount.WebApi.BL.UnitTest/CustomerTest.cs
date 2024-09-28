using BankAccount.WebApi.BL;
using BankAccount.WebAPI.DAL;
using Moq;

namespace BankAccount.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private Mock<ICustomerRepository> _mockCustomerRepository;
        private CustomerService _customerService;

        [SetUp]
        public void SetUp()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_mockCustomerRepository.Object);
        }

        [Test]
        public async Task GetCustomerByIdAsync_CustomerExists_ReturnsCustomer()
        {
            // Arrange
            var customerId = 1;
            var expectedCustomer = new Customer { CustomerId = customerId, FirstName = "John", LastName = "Doe" };
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync(expectedCustomer);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCustomer));
        }

        [Test]
        public void GetCustomerByIdAsync_CustomerDoesNotExist_ThrowsException()
        {
            // Arrange
            var customerId = 1;
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync((Customer)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _customerService.GetCustomerByIdAsync(customerId));
            Assert.That(ex.Message, Is.EqualTo("Customer not found."));
        }

        [Test]
        public async Task CreateCustomerAsync_ValidCustomer_ReturnsCreatedCustomer()
        {
            // Arrange
            var newCustomer = new Customer { FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = new DateTime(1990, 1, 1) };
            _mockCustomerRepository.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _customerService.CreateCustomerAsync(newCustomer.FirstName, newCustomer.LastName, newCustomer.Email, newCustomer.PhoneNumber, newCustomer.DateOfBirth);

            // Assert
            Assert.That(result.FirstName, Is.EqualTo(newCustomer.FirstName));
            Assert.That(result.LastName, Is.EqualTo(newCustomer.LastName));
            Assert.That(result.Email, Is.EqualTo(newCustomer.Email));
            Assert.That(result.PhoneNumber, Is.EqualTo(newCustomer.PhoneNumber));
            Assert.That(result.DateOfBirth, Is.EqualTo(newCustomer.DateOfBirth));
        }

        [Test]
        public async Task UpdateCustomerAsync_CustomerExists_UpdatesCustomer()
        {
            // Arrange
            var customerId = 1;
            var existingCustomer = new Customer { CustomerId = customerId, FirstName = "John", LastName = "Doe" };
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync(existingCustomer);
            _mockCustomerRepository.Setup(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            // Act
            await _customerService.UpdateCustomerAsync(customerId, "Jane", "Doe", "jane.doe@example.com", "1234567890");

            // Assert
            Assert.That(existingCustomer.FirstName, Is.EqualTo("Jane"));
            Assert.That(existingCustomer.LastName, Is.EqualTo("Doe"));
            Assert.That(existingCustomer.Email, Is.EqualTo("jane.doe@example.com"));
            Assert.That(existingCustomer.PhoneNumber, Is.EqualTo("1234567890"));
        }

        [Test]
        public void UpdateCustomerAsync_CustomerDoesNotExist_ThrowsException()
        {
            // Arrange
            var customerId = 1;
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByIdAsync(customerId)).ReturnsAsync((Customer)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _customerService.UpdateCustomerAsync(customerId, "Jane", "Doe", "jane.doe@example.com", "1234567890"));
            Assert.That(ex.Message, Is.EqualTo("Customer not found."));
        }

        [Test]
        public async Task DeleteCustomerAsync_CustomerExists_DeletesCustomer()
        {
            // Arrange
            var customerId = 1;
            _mockCustomerRepository.Setup(repo => repo.DeleteCustomerAsync(customerId)).Returns(Task.CompletedTask);

            // Act
            await _customerService.DeleteCustomerAsync(customerId);

            // Assert
            _mockCustomerRepository.Verify(repo => repo.DeleteCustomerAsync(customerId), Times.Once);
        }

        [Test]
        public async Task GetAllCustomersAsync_ReturnsAllCustomers()
        {
            // Arrange
            var expectedCustomers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Doe" }
            };
            _mockCustomerRepository.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(expectedCustomers);

            // Act
            var result = await _customerService.GetAllCustomersAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedCustomers));
        }
    }
}
