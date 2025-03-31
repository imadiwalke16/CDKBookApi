using Application.Domain.Entities;
using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceHistoryService : IServiceHistoryService
    {
        private readonly IServiceHistoryRepository _serviceHistoryRepository;

        public ServiceHistoryService(IServiceHistoryRepository serviceHistoryRepository)
        {
            _serviceHistoryRepository = serviceHistoryRepository;
        }

        public async Task<List<ServiceHistory>> GetServiceHistoryByVehicleIdAsync(int vehicleId)
        {
            return await _serviceHistoryRepository.GetServiceHistoryByVehicleIdAsync(vehicleId);
        }

        public async Task<ServiceHistory> AddServiceHistoryAsync(ServiceHistory serviceHistory)
        {
            return await _serviceHistoryRepository.AddServiceHistoryAsync(serviceHistory);
        }
    }
}
