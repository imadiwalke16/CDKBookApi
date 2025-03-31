using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ServiceAppointmentRepository : IServiceAppointmentRepository
    {
        private readonly AppDbContext _context;

        public ServiceAppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceAppointment> AddAsync(ServiceAppointment appointment)
        {
            _context.ServiceAppointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<ServiceAppointment?> GetByIdAsync(int id)
        {
            return await _context.ServiceAppointments
                .Include(sa => sa.User) // ✅ Fetch user details
                .Include(sa => sa.Vehicle) // ✅ Fetch vehicle details
                .Include(sa => sa.ServiceAppointmentServices)
                    .ThenInclude(sas => sas.ServiceOffered)
                .FirstOrDefaultAsync(sa => sa.Id == id);
        }

        public async Task<List<ServiceAppointment>> GetAllAsync()
        {
            return await _context.ServiceAppointments
                .Include(sa => sa.User) // ✅ Fetch user details
                .Include(sa => sa.Vehicle) // ✅ Fetch vehicle details
                .Include(sa => sa.ServiceAppointmentServices)
                    .ThenInclude(sas => sas.ServiceOffered)
                .ToListAsync();
        }
    }
}
