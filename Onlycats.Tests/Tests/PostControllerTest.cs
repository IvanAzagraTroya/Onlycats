using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlycatsTFG.models;
using OnlycatsTFG.PostService.Repositories;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using OnlycatsTFG;
using Onlycats.PostService.Models;
using OnlycatsTFG.PostService.Controllers;
using Onlycats.PostService.Services;

namespace Onlycats.Tests
{
    [TestFixture]
    public class PostsControllerTests
    {
        private Mock<IMongoRepository<Post, string>> _mockMongoRepository;
        private Mock<ILogger<PostsController>> _mockLogger;
        private Mock<ImageService> _mockImageService;
        private PostsController _postsController;

        [SetUp]
        public void Setup()
        {
            _mockMongoRepository = new Mock<IMongoRepository<Post, string>>();
            _mockLogger = new Mock<ILogger<PostsController>>();
            _mockImageService = new Mock<ImageService>();
            _postsController = new PostsController(_mockLogger.Object, _mockMongoRepository.Object, _mockImageService.Object);
        }

        [Test]
        public async Task ReadAllPosts_ReturnsOkResult_WithPosts()
        {
            // Arrange
            var posts = new List<Post> { new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 } };
            _mockMongoRepository.Setup(repo => repo.ReadAll()).ReturnsAsync(posts);

            // Act
            var result = await _postsController.ReadAllPosts();

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(posts));
        }

        [Test]
        public async Task ReadAllPosts_ReturnsNotFound_WhenNoPosts()
        {
            // Arrange
            var posts = new List<Post>();
            _mockMongoRepository.Setup(repo => repo.ReadAll()).ReturnsAsync(posts);

            // Act
            var result = await _postsController.ReadAllPosts();

            // Assert
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task ReadPostByIdAsync_ReturnsOkResult_WithPost()
        {
            // Arrange
            var post = new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 };
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync(post);

            // Act
            var result = await _postsController.ReadPostByIdAsync(post.Id);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(post));
        }

        [Test]
        public async Task ReadPostByIdAsync_ReturnsNotFound_WhenPostNotFound()
        {
            // Arrange
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync((Post)null);

            // Act
            var result = await _postsController.ReadPostByIdAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task UpdatePostAsync_ReturnsNoContent()
        {
            // Arrange
            var post = new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 };

            // Act
            var result = await _postsController.UpdatePostAsync(post);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeletePostAsync_ReturnsNoContent()
        {
            // Arrange
            var postId = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await _postsController.DeletePostAsync(postId);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task GetPostByUserIdAsync_ReturnsOkResult_WithPosts()
        {
            // Arrange
            var posts = new List<Post> { new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 } };
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<int>())).ReturnsAsync(posts);

            // Act
            var result = await _postsController.GetPostByUserIdAsync(1);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(posts));
        }

        [Test]
        public async Task GetPostByUserIdAsync_ReturnsNoContent_WhenNoPosts()
        {
            // Arrange
            var posts = new List<Post>();
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<int>())).ReturnsAsync(posts);

            // Act
            var result = await _postsController.GetPostByUserIdAsync(1);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteAllUserPosts_ReturnsNoContent()
        {
            // Arrange
            var posts = new List<Post> { new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 } };
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<int>())).ReturnsAsync(posts);

            // Act
            var result = await _postsController.DeleteAllUserPosts(1);

            // Assert
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public void GetPostImage_ReturnsNotFound_WhenImageDoesNotExist()
        {
            // Arrange
            var post = new Post { ImageUrl = "path/to/nonexistent.png" };

            // Act
            var result = _postsController.GetPostImage(post);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void GetPostImage_ReturnsBadRequest_WhenNoImageUrl()
        {
            // Arrange
            var post = new Post { ImageUrl = "" };

            // Act
            var result = _postsController.GetPostImage(post);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateLikes_ReturnsOkResult_WithUpdatedPost()
        {
            // Arrange
            var post = new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 };
            var request = new Request { IsLiked = true };
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync(post);

            // Act
            var result = await _postsController.UpdateLikes(post.Id, request);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(post));
        }

        [Test]
        public async Task UpdateLikes_ReturnsNotFound_WhenPostNotFound()
        {
            // Arrange
            var request = new Request { IsLiked = true };
            _mockMongoRepository.Setup(repo => repo.ReadByIdAsync(It.IsAny<string>())).ReturnsAsync((Post)null);

            // Act
            var result = await _postsController.UpdateLikes(ObjectId.GenerateNewId().ToString(), request);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetAllPostIds_ReturnsOkResult_WithPostIds()
        {
            // Arrange
            var posts = new List<Post>
    {
        new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 },
        new Post { Id = ObjectId.GenerateNewId().ToString(), UserId = 1, LikeNumber = 0 }
    };
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<int>())).ReturnsAsync(posts);

            // Act
            var result = await _postsController.GetAllPostIds(1);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            var ids = posts.Select(x => x.Id).ToList();
            Assert.That(okResult.Value, Is.EqualTo(ids));
        }

        [Test]
        public async Task GetAllPostIds_ReturnsOkResult_WithEmptyList_WhenNoPosts()
        {
            // Arrange
            var posts = new List<Post>();
            _mockMongoRepository.Setup(repo => repo.GetByOtherIdAsync(It.IsAny<int>())).ReturnsAsync(posts);

            // Act
            var result = await _postsController.GetAllPostIds(1);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(new List<string>()));
        }
    } 
}