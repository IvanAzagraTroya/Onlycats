using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Configuration;
using Onlycats.UserService.Controllers;
using Onlycats.UserService.DTOs;
using OnlycatsTFG.models;
using OnlycatsTFG.repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Onlycats.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IRepository<int, User>> _mockUserRepository;
        private Mock<IConfiguration> _mockConfig;
        private AuthController _authController;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IRepository<int, User>>();
            _mockConfig = new Mock<IConfiguration>();

            _mockConfig.Setup(config => config["Jwt:Key"]).Returns("myverystrongsecretkeythatexceeds32characters");
            _mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("Ivan");
            _mockConfig.Setup(config => config["Jwt:Audience"]).Returns("onlycats");

            _authController = new AuthController(_mockUserRepository.Object, _mockConfig.Object);
        }

        [Test]
        public void Login_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var userDto = new UserDTO { Email = "example@gmail.com", Password = "psw" };
            var user = new User("ex", "example", "example@gmail.com", "AQAAAAIAAYagAAAAEGsXhq0XXQtb70p11We1t1s8lNdY31r/y5G7JH9lOHVMbbhkwSTeMDKED2hiP4xLog==");

            // Mock the repository to return the user when GetByEmail is called
            _mockUserRepository.Setup(repo => repo.GetByEmail(userDto.Email)).Returns(user);

            // Act
            var result = _authController.Login(userDto);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Login_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var userDto = new UserDTO { Email = "invalid@example.com", Password = "password" };

            _mockUserRepository.Setup(repo => repo.GetByEmail(userDto.Email)).Returns((User)null);

            // Act
            var result = _authController.Login(userDto);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}