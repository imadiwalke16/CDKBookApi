using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Application.Interfaces;
using Application.Services;
using Application.Domain.Entities;

namespace CDKBookAPI.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IJwtProvider> _mockJwtProvider;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockJwtProvider = new Mock<IJwtProvider>();
            _authService = new AuthService(_mockUserRepo.Object, _mockJwtProvider.Object);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password123";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            string fakeToken = "fake-jwt-token";

            var user = new User { Id = 1, Email = email, PasswordHash = hashedPassword, Role = "User" };

            _mockUserRepo.Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(user);

            _mockJwtProvider.Setup(jwt => jwt.GenerateToken(email, user.Id, user.Role))
                .Returns(fakeToken);

            // Act
            var result = await _authService.LoginAsync(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeToken, result);
        }

        [Fact] 
        public async Task LoginAsync_InvalidPassword_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            string email = "test@example.com";
            string password = "wrongpassword";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("correctpassword");

            var user = new User { Id = 1, Email = email, PasswordHash = hashedPassword, Role = "User" };

            _mockUserRepo.Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(user);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(email, password));
        }

        [Theory]
        [InlineData("", "password123")]
        [InlineData("test@example.com", "")]
        [InlineData("", "")]
        public async Task LoginAsync_EmptyEmailOrPassword_ThrowsArgumentException(string email, string password)
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _authService.LoginAsync(email, password));
        }


        [Fact]
        public async Task RegisterAsync_ValidData_ReturnsToken()
        {
            // Arrange
            string email = "newuser@example.com";
            string password = "password123";
            string role = "User";
            string fakeToken = "fake-jwt-token";

            _mockUserRepo.Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync((User)null); // No existing user

            _mockJwtProvider.Setup(jwt => jwt.GenerateToken(email, It.IsAny<int>(), role))
                .Returns(fakeToken);

            _mockUserRepo.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _authService.RegisterAsync(email, password, role);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeToken, result);
        }

        [Fact]
        public async Task RegisterAsync_DuplicateEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            string email = "existing@example.com";
            string password = "password123";
            string role = "User";

            var existingUser = new User { Id = 1, Email = email, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password), Role = role };

            _mockUserRepo.Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(existingUser); // User already exists

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.RegisterAsync(email, password, role));
        }

        [Theory]
        [InlineData("", "password123", "User")]
        [InlineData("test@example.com", "", "User")]
        [InlineData("", "", "User")]
        public async Task RegisterAsync_EmptyEmailOrPassword_ThrowsArgumentException(string email, string password, string role)
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _authService.RegisterAsync(email, password, role));
        }

    }
}