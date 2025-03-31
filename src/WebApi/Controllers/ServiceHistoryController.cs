using Application.Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceHistoryController : ControllerBase
    {
        private readonly IServiceHistoryService _serviceHistoryService;

        public ServiceHistoryController(IServiceHistoryService serviceHistoryService)
        {
            _serviceHistoryService = serviceHistoryService;
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetServiceHistoryByVehicleId(int vehicleId)
        {
            var serviceHistory = await _serviceHistoryService.GetServiceHistoryByVehicleIdAsync(vehicleId);

            if (serviceHistory == null || serviceHistory.Count == 0)
                return NotFound(new { message = "No service history found for this vehicle." });

            return Ok(serviceHistory);
        }

        // ✅ New POST Endpoint to Add Service History
        [HttpPost]
        public async Task<IActionResult> AddServiceHistory([FromBody] ServiceHistory serviceHistory)
        {
            if (serviceHistory == null)
                return BadRequest(new { message = "Invalid data." });

            var addedServiceHistory = await _serviceHistoryService.AddServiceHistoryAsync(serviceHistory);
            return CreatedAtAction(nameof(GetServiceHistoryByVehicleId), new { vehicleId = serviceHistory.VehicleId }, addedServiceHistory);
        }
    }
}
