using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceAppointmentRepository
    {
        Task<ServiceAppointment> AddAsync(ServiceAppointment appointment);
        Task<ServiceAppointment?> GetByIdAsync(int id);
        Task<List<ServiceAppointment>> GetAllAsync();
    }
}
