using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ServiceCenterRepository : IServiceCenterRepository
    {
        private readonly AppDbContext _context;

        public ServiceCenterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceCenter>> GetByPinCodeAsync(string pinCode)
        {
            return await _context.ServiceCenters
                .Where(sc => sc.PinCode == pinCode)
                .ToListAsync();
        }
    }
}
