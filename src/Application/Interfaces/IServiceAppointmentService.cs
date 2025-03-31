using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.ServiceAppointment;

namespace Application.Interfaces
{
    public interface IServiceAppointmentService
    {
        Task<ServiceAppointmentDTO> CreateAsync(ServiceAppointmentCreateDTO dto);
        Task<ServiceAppointmentDTO?> GetByIdAsync(int id);
        Task<List<ServiceAppointmentDTO>> GetAllAsync();
    }
}
