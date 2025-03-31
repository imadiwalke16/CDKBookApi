using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.Domain.Entities;
using System;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]  // Ensure Swagger knows this returns JSON
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// User Login
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(new { Token = token });
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
            Console.WriteLine($"Extracted Email from Token: {email}"); // Debugging line

            if (string.IsNullOrEmpty(email)) return Unauthorized();

            var user = await _userRepository.GetByEmailAsync(email); // FIXED: Corrected method name
            if (user == null)
            {
                Console.WriteLine("User not found in database"); // Debugging line
                return NotFound("User not found");
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.PhoneNumber,

        });

        }
    }

    public record LoginRequest(string Email, string Password);
}
