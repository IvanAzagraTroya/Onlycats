using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onlycats.InteractionService.Controllers;
using OnlycatsTFG.models;
using OnlycatsTFG.repository.mongorepository;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onlycats.Tests
{
    [TestFixture]
    public class InteractionsControllerTests
    {
        private Mock<IMongoRepository<Activity, ObjectId>> _mockMongoRepository;
        private Mock<ILogger<InteractionsController>> _mockLogger;
        private InteractionsController _interactionsController;

        [SetUp]
        public void Setup()
        {
            _mockMongoRepository = new Mock<IMongoRepository<Activity, ObjectId>>();
            _mockLogger = new Mock<ILogger<InteractionsController>>();
            _interactionsController = new InteractionsController(_mockMongoRepository.Object);
        }

        [Test]
        public async Task ReadAll_ReturnsOkResult_WithInteractions()
        {
            // Arrange
            var interactions = new List<Activity> { new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 } };
            _mockMongoRepository.Setup(repo => repo.ReadAll()).ReturnsAsync(interactions);

            // Act
            var result = await _interactionsController.ReadAll();

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(interactions));
        }

        [Test]
        public async Task ReadInteractionByIdAsync_ReturnsOkResult_WithInteraction()
        {
            // Arrange
            var interaction = new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 };
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(interaction);

            // Act
            var result = await _interactionsController.ReadInteractionByIdAsync(interaction.Id);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(interaction));
        }

        [Test]
        public async Task ReadInteractionByIdAsync_ReturnsNoContent_WhenInteractionNotFound()
        {
            // Arrange
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync((Activity)null);

            // Act
            var result = await _interactionsController.ReadInteractionByIdAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            Assert.That(result.Result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task AddInteractionAsync_ReturnsCreatedResult()
        {
            // Arrange
            var interaction = new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 };

            // Act
            var result = await _interactionsController.AddInteractionAsync(interaction);

            // Assert
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public async Task UpdateInteractionAsync_ReturnsNoContent()
        {
            // Arrange
            var interaction = new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 };

            // Act
            var result = await _interactionsController.UpdateInteractionAsync(interaction);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteInteractionAsyncs_ReturnsNoContent()
        {
            // Arrange
            var interactionId = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await _interactionsController.DeleteInteractionAsyncs(interactionId);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task GetInteractionsByUserId_ReturnsOkResult_WithInteractions()
        {
            // Arrange
            var interactions = new List<Activity> { new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 } };
            _mockMongoRepository.Setup(repo => repo.GetInteractionsByUserId(It.IsAny<int>())).ReturnsAsync(interactions);

            // Act
            var result = await _interactionsController.GetInteractionsByUserId(1);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(interactions));
        }

        [Test]
        public async Task GetPostInteractionsOrderedByDateAsync_ReturnsOkResult_WithActivities()
        {
            // Arrange
            var activities = new List<Activity> { new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 } };
            _mockMongoRepository.Setup(repo => repo.GetPostInteractionsOrderedByDateAsync(It.IsAny<ObjectId>())).ReturnsAsync(activities);

            // Act
            var result = await _interactionsController.GetPostInteractionsOrderedByDateAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(activities));
        }

        [Test]
        public async Task GetByActivityType_ReturnsOkResult_WithActivities()
        {
            // Arrange
            var activities = new List<Activity> { new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 } };
            _mockMongoRepository.Setup(repo => repo.GetByInteractionType(It.IsAny<int>())).ReturnsAsync(activities);

            // Act
            var result = await _interactionsController.GetByActivityType(1);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(activities));
        }

        [Test]
        public async Task GetByPostIdAsync_ReturnsOkResult_WithActivities()
        {
            // Arrange
            var activities = new List<Activity> { new Activity { Id = ObjectId.GenerateNewId().ToString(), PostId = ObjectId.GenerateNewId().ToString(), UserId = 1 } };
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(activities);

            // Act
            var result = await _interactionsController.GetByPostIdAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(activities));
        }
    }
}