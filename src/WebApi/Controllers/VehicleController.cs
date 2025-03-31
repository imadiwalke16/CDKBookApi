using Application.Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetVehiclesByUserId(int userId)
        {
            var vehicles = await _vehicleService.GetVehiclesByUserIdAsync(userId);
            return Ok(vehicles);
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicleById(int vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            await _vehicleService.AddVehicleAsync(vehicle);
            return CreatedAtAction(nameof(GetVehicleById), new { vehicleId = vehicle.Id }, vehicle);
        }

        [HttpPut("{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromBody] Vehicle updatedVehicle)
        {
            var existingVehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (existingVehicle == null) return NotFound();

            existingVehicle.Make = updatedVehicle.Make;
            existingVehicle.Model = updatedVehicle.Model;
            existingVehicle.Year = updatedVehicle.Year;
            existingVehicle.LicensePlate = updatedVehicle.LicensePlate;

            await _vehicleService.UpdateVehicleAsync(existingVehicle);
            return NoContent();
        }

        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            await _vehicleService.DeleteVehicleAsync(vehicleId);
            return NoContent();
        }
    }
}
