using Application.Domain.Entities;
using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<Vehicle>> GetVehiclesByUserIdAsync(int userId)
        {
            return await _vehicleRepository.GetVehiclesByUserIdAsync(userId);
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
        {
            return await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _vehicleRepository.AddVehicleAsync(vehicle);
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            await _vehicleRepository.UpdateVehicleAsync(vehicle);
        }

        public async Task DeleteVehicleAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            if (vehicle != null)
            {
                await _vehicleRepository.DeleteVehicleAsync(vehicle);
            }
        }
    }
}
