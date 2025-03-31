using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Domain.Entities;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        //if block remove if found any error 
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Email and password cannot be empty.");
        }

        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        return _jwtProvider.GenerateToken(user.Email, user.Id, user.Role);
    }

    public async Task<string> RegisterAsync(string email, string password, string role)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Email and password cannot be empty.");
        }

        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            Email = email,
            PasswordHash = hashedPassword,
            Role = role
        };

        await _userRepository.AddAsync(newUser);

        return _jwtProvider.GenerateToken(newUser.Email, newUser.Id, newUser.Role);
    }

}
