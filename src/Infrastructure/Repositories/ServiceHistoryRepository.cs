using Application.Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ServiceHistoryRepository : IServiceHistoryRepository
    {
        private readonly AppDbContext _context;

        public ServiceHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceHistory>> GetServiceHistoryByVehicleIdAsync(int vehicleId)
        {
            return await _context.ServiceHistories
                .Where(sh => sh.VehicleId == vehicleId)
                .ToListAsync();
        }

        public async Task<ServiceHistory> AddServiceHistoryAsync(ServiceHistory serviceHistory)
        {
            _context.ServiceHistories.Add(serviceHistory);
            await _context.SaveChangesAsync();
            return serviceHistory;
        }
    }
}
