using OnlycatsTFG.PostService.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlycatsTFG.models;
using OnlycatsTFG.PostService.Controllers;
using Onlycats.PostService.Models;

namespace Onlycats.Tests
{
    [TestFixture]
    public class CommentsControllerTests
    {
        private Mock<IMongoRepository<Comment, ObjectId>> _mockMongoRepository;
        private Mock<ILogger<CommentsController>> _mockLogger;
        private CommentsController _commentsController;

        [SetUp]
        public void Setup()
        {
            _mockMongoRepository = new Mock<IMongoRepository<Comment, ObjectId>>();
            _mockLogger = new Mock<ILogger<CommentsController>>();
            _commentsController = new CommentsController(_mockLogger.Object, _mockMongoRepository.Object);
        }

        [Test]
        public async Task ReadAllComments_ReturnsOkResult_WithComments()
        {
            // Arrange
            var comments = new List<Comment> { new Comment { Id = ObjectId.GenerateNewId().ToString(), Content = "comment content", Displayname = "testname", Username = "testname", PostId = ObjectId.GenerateNewId().ToString(), UserId = 1, Likes = 0 } };
            _mockMongoRepository.Setup(repo => repo.ReadAll()).ReturnsAsync(comments);

            // Act
            var result = await _commentsController.ReadAllComments();

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(comments));
        }

        [Test]
        public async Task ReadAllComments_ReturnsNotFound_WhenNoComments()
        {
            // Arrange
            var comments = new List<Comment>();
            _mockMongoRepository.Setup(repo => repo.ReadAll()).ReturnsAsync(comments);

            // Act
            var result = await _commentsController.ReadAllComments();

            // Assert
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task ReadCommentByIdAsync_ReturnsOkResult_WithComment()
        {
            // Arrange
            var comment = new Comment { Id = ObjectId.GenerateNewId().ToString(), Content = "comment content", Displayname = "testname", Username = "testname", PostId = ObjectId.GenerateNewId().ToString(), UserId = 1, Likes = 0 };
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync(comment);

            // Act
            var result = await _commentsController.ReadCommentByIdAsync(comment.Id);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(comment));
        }

        [Test]
        public async Task ReadCommentByIdAsync_ReturnsNotFound_WhenCommentNotFound()
        {
            // Arrange
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync((Comment)null);

            // Act
            var result = await _commentsController.ReadCommentByIdAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task AddCommentAsync_ReturnsCreatedResult()
        {
            // Arrange
            var comment = new Comment { Id = ObjectId.GenerateNewId().ToString(), Content = "comment content", Displayname = "testname", Username = "testname", PostId = ObjectId.GenerateNewId().ToString(), UserId = 1, Likes = 0 };

            // Act
            var result = await _commentsController.AddCommentAsync(comment);

            // Assert
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public async Task DeleteCommentAsync_ReturnsNoContent()
        {
            // Arrange
            var commentId = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await _commentsController.DeleteCommentAsync(commentId);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task GetCommentByPostIdAsync_ReturnsOkResult_WithComments()
        {
            // Arrange
            var comments = new List<Comment> { new Comment { Id = ObjectId.GenerateNewId().ToString(), Content = "comment content", Displayname = "testname", Username = "testname", PostId = ObjectId.GenerateNewId().ToString(), UserId = 1, Likes = 0 } };
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(comments);

            // Act
            var result = await _commentsController.GetCommentByPostIdAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(comments));
        }

        [Test]
        public async Task GetCommentByPostIdAsync_ReturnsNoContent_WhenNoComments()
        {
            // Arrange
            var comments = new List<Comment>();
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(comments);

            // Act
            var result = await _commentsController.GetCommentByPostIdAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            Assert.That(result.Result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task UpdateLikes_ReturnsOkResult_WithUpdatedComment()
        {
            // Arrange
            var comment = new Comment { Id = ObjectId.GenerateNewId().ToString(), Content = "comment content", Displayname = "testname", Username = "testname", PostId = ObjectId.GenerateNewId().ToString(), UserId = 1, Likes = 0 };
            var request = new Request { IsLiked = true };
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync(comment);

            // Act
            var result = await _commentsController.UpdateLikes(comment.Id, request);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(comment));
        }

        [Test]
        public async Task UpdateLikes_ReturnsNotFound_WhenCommentNotFound()
        {
            // Arrange
            var request = new Request { IsLiked = true };
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync((Comment)null);

            // Act
            var result = await _commentsController.UpdateLikes(ObjectId.GenerateNewId().ToString(), request);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
    }
}