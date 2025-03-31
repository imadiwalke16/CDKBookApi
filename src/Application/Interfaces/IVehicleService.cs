using Application.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetVehiclesByUserIdAsync(int userId);
        Task<Vehicle?> GetVehicleByIdAsync(int vehicleId);
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int vehicleId);
    }
}
