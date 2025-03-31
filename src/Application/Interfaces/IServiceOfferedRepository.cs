using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceOfferedRepository
    {
        Task<List<ServiceOffered>> GetServicesByServiceCenterIdAsync(int serviceCenterId);
        Task<ServiceOffered?> GetByIdAsync(int id);  // ✅ Add this method
    }
}
