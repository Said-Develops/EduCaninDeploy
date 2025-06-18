using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EduCanin.Tests.Service
{
    [TestClass]
    public class ApplicationUserServiceTests
    {
        private Mock<IApplicationUserRepository> _userRepositoryMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private ApplicationUserService _service;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IApplicationUserRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _service = new ApplicationUserService(_userRepositoryMock.Object, _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public async Task AddAsync_ShouldCallAddAndSaveChanges()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            await _service.AddAsync(user);

            // Assert
            _userRepositoryMock.Verify(r => r.AddAsync(user), Times.Once);
            _userRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteUser_WhenUserExists()
        {
            // Arrange
            var user = new ApplicationUser();
            _userRepositoryMock.Setup(r => r.GetByIdAsync("abc")).ReturnsAsync(user);

            // Act
            await _service.DeleteAsync("abc");

            // Assert
            _userRepositoryMock.Verify(r => r.Delete(user), Times.Once);
            _userRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDoNothing_WhenUserNotFound()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetByIdAsync("xyz")).ReturnsAsync((ApplicationUser)null);

            // Act
            await _service.DeleteAsync("xyz");

            // Assert
            _userRepositoryMock.Verify(r => r.Delete(It.IsAny<ApplicationUser>()), Times.Never);
            _userRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<ApplicationUser> { new ApplicationUser(), new ApplicationUser() };
            _userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.AreEqual(2, ((List<ApplicationUser>)result).Count);
        }

        [TestMethod]
        public async Task GetCurrentUserWithDogsAsync_ShouldReturnUser_WhenUserIsLogged()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "123") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            var context = new DefaultHttpContext { User = principal };
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);

            var expectedUser = new ApplicationUser();
            _userRepositoryMock.Setup(r => r.GetUserWithDogsByIdAsync("123")).ReturnsAsync(expectedUser);

            // Act
            var result = await _service.GetCurrentUserWithDogsAsync();

            // Assert
            Assert.AreEqual(expectedUser, result);
        }

        [TestMethod]
        public async Task GetCurrentUserWithDogsAsync_ShouldReturnNull_WhenUserIsNull()
        {
            // Arrange
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            // Act
            var result = await _service.GetCurrentUserWithDogsAsync();

            // Assert
            Assert.IsNull(result);
        }
    }
}