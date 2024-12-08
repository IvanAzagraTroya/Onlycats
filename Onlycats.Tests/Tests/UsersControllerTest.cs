using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Onlycats.UserService.Controllers;
using OnlycatsTFG.models;
using OnlycatsTFG.repository;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Onlycats.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IRepository<int, User>> _mockUserRepository;
        private UsersController _usersController;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IRepository<int, User>>();
            _usersController = new UsersController(_mockUserRepository.Object);
        }

        [Test]
        public async Task GetAllUsersAsync_ReturnsOkResult_WithUsers()
        {
            // Arrange
            var users = new List<User> { new User("testuser", "testuser", "test@example.com", "password") };
            _mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _usersController.GetAllUsersAsync();

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(users));
        }

        [Test]
        public async Task GetAllUsersAsync_ReturnsNotFound_WhenNoUsers()
        {
            // Arrange
            var users = new List<User>();
            _mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _usersController.GetAllUsersAsync();

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task AddUserAsync_ReturnsCreatedResult()
        {
            // Arrange
            var user = new User("testuser", "testuser", "test@example.com", "password");
            var fileMock = new Mock<IFormFile>();

            // Act
            var result = await _usersController.AddUserAsync(user, fileMock.Object);

            // Assert
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public async Task UpdateUserAsync_ReturnsOkResult()
        {
            // Arrange
            var user = new User("testuser", "testuser", "test@example.com", "password");

            // Act
            var result = await _usersController.UpdateUserAsync(user);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(user));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsOkResult_WithUser()
        {
            // Arrange
            var user = new User("testuser", "testuser", "test@example.com", "password");
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _usersController.GetByIdAsync(1);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(user));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(9999)).ReturnsAsync((User)null);

            // Act
            var result = await _usersController.GetByIdAsync(9999);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void GetByEmail_ReturnsOkResult_WithUser()
        {
            // Arrange
            var user = new User("testuser", "testuser", "test@example.com", "password");
            _mockUserRepository.Setup(repo => repo.GetByEmail("test@example.com")).Returns(user);

            // Act
            var result = _usersController.GetByEmail("test@example.com");

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(user));
        }

        [Test]
        public void GetByEmail_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetByEmail("testssss@example.com")).Returns((User)null);

            // Act
            var result = _usersController.GetByEmail("testssss@example.com");

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
    }
}