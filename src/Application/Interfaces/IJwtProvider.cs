using System;
namespace Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(string email, int userId, string role);
}
