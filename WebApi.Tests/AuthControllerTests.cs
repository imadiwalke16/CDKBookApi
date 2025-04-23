using System.Security.Claims;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _controller = new AuthController(_authServiceMock.Object, _userRepositoryMock.Object);
        }

        // Tests for POST /api/auth/login
        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var request = new LoginRequest("test@example.com", "password");
            var token = "jwt_token";
            _authServiceMock.Setup(s => s.LoginAsync(request.Email, request.Password))
                .ReturnsAsync(token);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var response = okResult.Value;
            Assert.NotNull(response);
            Assert.Equal(token, response.GetType().GetProperty("Token")?.GetValue(response));
            _authServiceMock.Verify(s => s.LoginAsync(request.Email, request.Password), Times.Once());
        }

        //[Fact]
        //public async Task Login_InvalidCredentials_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var request = new LoginRequest("test@example.com", "wrong_password");
        //    _authServiceMock.Setup(s => s.LoginAsync(request.Email, request.Password))
        //        .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials."));

        //    // Act
        //    var result = await _controller.Login(request);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        //    Assert.Equal("Invalid credentials.", badRequestResult.Value);
        //    _authServiceMock.Verify(s => s.LoginAsync(request.Email, request.Password), Times.Once());
        //}

        //[Fact]
        //public async Task Login_EmptyEmailOrPassword_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var request = new LoginRequest("", "password");
        //    _authServiceMock.Setup(s => s.LoginAsync(request.Email, request.Password))
        //        .ThrowsAsync(new ArgumentException("Email and password cannot be empty."));

        //    // Act
        //    var result = await _controller.Login(request);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        //    Assert.Equal("Email and password cannot be empty.", badRequestResult.Value);
        //    _authServiceMock.Verify(s => s.LoginAsync(request.Email, request.Password), Times.Once());
        //}

        // Tests for GET /api/auth/me
        [Fact]
        public async Task GetUserDetails_AuthenticatedUser_ReturnsOkWithUserDetails()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User
            {
                Id = 1,
                Name = "Test User",
                Email = email,
                PhoneNumber = "1234567890",
                Role = "Customer"
            };
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(email))
                .ReturnsAsync(user);

            // Simulate authenticated user
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.GetUserDetails();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var response = okResult.Value;
            Assert.NotNull(response);
            Assert.Equal(user.Id, response.GetType().GetProperty("Id")?.GetValue(response));
            Assert.Equal(user.Name, response.GetType().GetProperty("Name")?.GetValue(response));
            Assert.Equal(user.Email, response.GetType().GetProperty("Email")?.GetValue(response));
            Assert.Equal(user.PhoneNumber, response.GetType().GetProperty("PhoneNumber")?.GetValue(response));
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(email), Times.Once());
        }

        [Fact]
        public async Task GetUserDetails_UnauthenticatedUser_ReturnsUnauthorized()
        {
            // Arrange
            // No claims set (simulating unauthenticated user)
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            // Act
            var result = await _controller.GetUserDetails();

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task GetUserDetails_NonExistingUser_ReturnsNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(email))
                .ReturnsAsync((User)null);

            // Simulate authenticated user
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.GetUserDetails();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.Equal("User not found", notFoundResult.Value);
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(email), Times.Once());
        }

        [Fact]
        public async Task GetUserDetails_NullEmailInToken_ReturnsUnauthorized()
        {
            // Arrange
            // Simulate authenticated user with no email claim
            var claims = new Claim[] { };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.GetUserDetails();

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Never());
        }
    }
}