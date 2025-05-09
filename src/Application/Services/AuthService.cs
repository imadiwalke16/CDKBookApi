using Application.Interfaces;
using Application.Domain.Entities;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> LoginAsync(string email, string password, int? dealerId = null)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");

            if (dealerId.HasValue && user.DealerId != dealerId)
            {
                user.DealerId = dealerId;
                await _userRepository.UpdateAsync(user); // Update user in DB
            }

            return _jwtProvider.GenerateToken(user.Email, user.Id, user.Role);
        }
    }
}