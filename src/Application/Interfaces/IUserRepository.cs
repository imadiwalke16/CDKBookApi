using System;
using Application.Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        // Add this for promotional notifications
        Task<List<User>> GetAllAsync();
        // Add other methods you might already hav
    }
}