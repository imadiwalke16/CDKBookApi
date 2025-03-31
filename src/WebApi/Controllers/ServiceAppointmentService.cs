using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.ServiceAppointment;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/service-appointments")]
    public class ServiceAppointmentController : ControllerBase
    {
        private readonly IServiceAppointmentService _serviceAppointmentService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ServiceAppointmentController(
             IServiceAppointmentService serviceAppointmentService,
             IHubContext<NotificationHub> hubContext)
        {
            _serviceAppointmentService = serviceAppointmentService;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Create a new service appointment
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceAppointmentCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceAppointmentService.CreateAsync(dto);

            await _hubContext.Clients.User(dto.UserId.ToString())
                .SendAsync("ReceiveNotification", dto.UserId, "Appointment Booked",
                    $"Your appointment on {dto.AppointmentDate} has been scheduled!");
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Get a service appointment by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceAppointmentService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { Message = "Service appointment not found" });

            return Ok(result);
        }

        /// <summary>
        /// Get all service appointments
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _serviceAppointmentService.GetAllAsync();
            return Ok(results);
        }
    }
}
