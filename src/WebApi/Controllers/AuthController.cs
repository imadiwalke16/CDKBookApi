using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.Domain.Entities;
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent; // Add for ConcurrentDictionary

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private static readonly ConcurrentDictionary<string, int> _sessionTokens = new ConcurrentDictionary<string, int>(); // Store sessionToken -> DealerId

        public AuthController(IAuthService authService, IUserRepository userRepository, AppDbContext dbContext)
        {
            _authService = authService;
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Validate Dealer Code
        /// </summary>
        [HttpPost("validate-code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ValidateCode([FromBody] ValidateCodeRequest request)
        {
            if (string.IsNullOrEmpty(request.Code))
                return BadRequest("Dealer code is required");

            var dealer = _dbContext.Dealers
                .FirstOrDefault(d => d.Code == request.Code);
            if (dealer == null)
                return BadRequest("Invalid dealer code");

            var sessionToken = Guid.NewGuid().ToString();
            _sessionTokens[sessionToken] = dealer.DealerId;

            return Ok(new
            {
                DealerId = dealer.DealerId,
                Name = dealer.Name,
                ThemeConfig = dealer.ThemeConfig,
                SessionToken = sessionToken
            });
        }

        /// <summary>
        /// User Login
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                Console.WriteLine($"SessionTokens: {string.Join(", ", _sessionTokens.Keys)}"); // Debug
                if (!_sessionTokens.TryGetValue(request.SessionToken, out var dealerId))
                    return BadRequest("Invalid or expired session token");

                var token = await _authService.LoginAsync(request.Email, request.Password, dealerId);
                return Ok(new { Token = token, DealerId = dealerId });
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest("Invalid email or password");
            }
        }

        /// <summary>
        /// Get Logged-In User Details
        /// </summary>
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserDetails()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            Console.WriteLine($"Extracted Email from Token: {email}");

            if (string.IsNullOrEmpty(email)) return Unauthorized();

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                Console.WriteLine("User not found in database");
                return NotFound("User not found");
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.PhoneNumber
            });
        }
    }

    public record LoginRequest(string Email, string Password, string SessionToken);

    public record ValidateCodeRequest(string Code);
}