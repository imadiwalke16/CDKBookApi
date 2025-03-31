using Application.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceHistoryRepository
    {
        Task<List<ServiceHistory>> GetServiceHistoryByVehicleIdAsync(int vehicleId);
        Task<ServiceHistory> AddServiceHistoryAsync(ServiceHistory serviceHistory); // ✅ New Method
    }
}
