using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ServiceAppointment;
using Application.DTOs.User;
using Application.DTOs.Vehicle;
using Application.Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class ServiceAppointmentService : IServiceAppointmentService
    {
        private readonly IServiceAppointmentRepository _repository;
        private readonly IServiceOfferedRepository _serviceOfferedRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public ServiceAppointmentService(
            IServiceAppointmentRepository repository,
            IServiceOfferedRepository serviceOfferedRepository,
            IUserRepository userRepository,
            IVehicleRepository vehicleRepository)
        {
            _repository = repository;
            _serviceOfferedRepository = serviceOfferedRepository;
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ServiceAppointmentDTO> CreateAsync(ServiceAppointmentCreateDTO dto)
        {
            var appointment = new ServiceAppointment
            {
                AppointmentDate = dto.AppointmentDate,
                Status = "Scheduled",
                UserId = dto.UserId,
                VehicleId = dto.VehicleId,
                ServiceCenterId = dto.ServiceCenterId,
                TransportMode = dto.TransportMode,
                TransportCharge = dto.TransportCharge,
                ServiceAppointmentServices = new List<Application.Domain.Entities.ServiceAppointmentService>() // ✅ Fixed type issue
            };

            // ✅ Link selected services
            foreach (var serviceOfferedId in dto.ServiceOfferedIds)
            {
                var service = await _serviceOfferedRepository.GetByIdAsync(serviceOfferedId);
                if (service != null)
                {
                    appointment.ServiceAppointmentServices.Add(new Application.Domain.Entities.ServiceAppointmentService
                    {
                        ServiceAppointment = appointment,
                        ServiceOffered = service
                    });
                }
            }

            var createdAppointment = await _repository.AddAsync(appointment);

            // ✅ Fetch User and Vehicle details
            var user = await _userRepository.GetByIdAsync(createdAppointment.UserId);
            var vehicle = await _vehicleRepository.GetByIdAsync(createdAppointment.VehicleId);

            return new ServiceAppointmentDTO
            {
                Id = createdAppointment.Id,
                AppointmentDate = createdAppointment.AppointmentDate,
                Status = createdAppointment.Status,
                UserId = createdAppointment.UserId,
                VehicleId = createdAppointment.VehicleId,
                ServiceCenterId = createdAppointment.ServiceCenterId,
                TransportMode = createdAppointment.TransportMode,
                TransportCharge = createdAppointment.TransportCharge,

                // ✅ Map User Details
                User = user != null
                    ? new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    }
                    : null,

                // ✅ Map Vehicle Details
                Vehicle = vehicle != null
                    ? new VehicleDTO
                    {
                        Id = vehicle.Id,
                        Model = vehicle.Model,
                        LicensePlate = vehicle.LicensePlate
                    }
                    : null,

                // ✅ Map Selected Services
                ServiceAppointmentServices = createdAppointment.ServiceAppointmentServices
                    .Select(sas => new ServiceAppointmentServiceDTO
                    {
                        ServiceOfferedId = sas.ServiceOfferedId,
                        ServiceName = sas.ServiceOffered.Name
                    })
                    .ToList()
            };
        }

        public async Task<ServiceAppointmentDTO?> GetByIdAsync(int id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            if (appointment == null) return null;

            return new ServiceAppointmentDTO
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                UserId = appointment.UserId,
                VehicleId = appointment.VehicleId,
                ServiceCenterId = appointment.ServiceCenterId,
                TransportMode = appointment.TransportMode,
                TransportCharge = appointment.TransportCharge,

                // ✅ Map User Details
                User = new UserDTO
                {
                    Id = appointment.User.Id,
                    Name = appointment.User.Name,
                    Email = appointment.User.Email
                },

                // ✅ Map Vehicle Details
                Vehicle = new VehicleDTO
                {
                    Id = appointment.Vehicle.Id,
                    Model = appointment.Vehicle.Model,
                    LicensePlate = appointment.Vehicle.LicensePlate
                },

                // ✅ Map Selected Services
                ServiceAppointmentServices = appointment.ServiceAppointmentServices
                    .Select(sas => new ServiceAppointmentServiceDTO
                    {
                        ServiceOfferedId = sas.ServiceOfferedId,
                        ServiceName = sas.ServiceOffered.Name
                    })
                    .ToList()
            };
        }

        public async Task<List<ServiceAppointmentDTO>> GetAllAsync()
        {
            var appointments = await _repository.GetAllAsync();
            return appointments.Select(appointment => new ServiceAppointmentDTO
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                UserId = appointment.UserId,
                VehicleId = appointment.VehicleId,
                ServiceCenterId = appointment.ServiceCenterId,
                TransportMode = appointment.TransportMode,
                TransportCharge = appointment.TransportCharge,

                // ✅ Map User Details
                User = new UserDTO
                {
                    Id = appointment.User.Id,
                    Name = appointment.User.Name,
                    Email = appointment.User.Email
                },

                // ✅ Map Vehicle Details
                Vehicle = new VehicleDTO
                {
                    Id = appointment.Vehicle.Id,
                    Model = appointment.Vehicle.Model,
                    LicensePlate = appointment.Vehicle.LicensePlate
                },

                // ✅ Map Selected Services
                ServiceAppointmentServices = appointment.ServiceAppointmentServices
                    .Select(sas => new ServiceAppointmentServiceDTO
                    {
                        ServiceOfferedId = sas.ServiceOfferedId,
                        ServiceName = sas.ServiceOffered.Name
                    })
                    .ToList()
            }).ToList();
        }
    }
}
