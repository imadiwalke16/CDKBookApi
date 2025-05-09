using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password, int? dealerId = null);
    }
}