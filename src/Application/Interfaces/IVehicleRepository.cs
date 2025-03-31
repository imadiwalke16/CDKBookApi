using Application.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetVehiclesByUserIdAsync(int userId);
        Task<Vehicle?> GetVehicleByIdAsync(int vehicleId);
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);
        Task<Vehicle?> GetByIdAsync(int id);

    }
}
