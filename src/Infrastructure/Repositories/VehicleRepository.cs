using Application.Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetVehiclesByUserIdAsync(int userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == vehicleId);
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }
    }
}
