using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ServiceOfferedRepository : IServiceOfferedRepository
    {
        private readonly AppDbContext _context;

        public ServiceOfferedRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceOffered>> GetServicesByServiceCenterIdAsync(int serviceCenterId)
        {
            return await _context.ServicesOffered
                .Where(s => s.ServiceCenterId == serviceCenterId)
                .ToListAsync();
        }

        public async Task<ServiceOffered?> GetByIdAsync(int id) // ✅ Implemented method
        {
            return await _context.ServicesOffered.FindAsync(id);
        }
    }
}
