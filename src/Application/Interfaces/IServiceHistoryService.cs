using System;
using Application.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceHistoryService
    {
        Task<List<ServiceHistory>> GetServiceHistoryByVehicleIdAsync(int vehicleId);
        Task<ServiceHistory> AddServiceHistoryAsync(ServiceHistory serviceHistory); // ✅ New Method
    }
}
