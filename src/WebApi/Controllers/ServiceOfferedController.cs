using System;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/service-offered")]
    public class ServiceOfferedController : ControllerBase
    {
        private readonly IServiceOfferedRepository _serviceOfferedRepository;

        public ServiceOfferedController(IServiceOfferedRepository serviceOfferedRepository)
        {
            _serviceOfferedRepository = serviceOfferedRepository;
        }

        /// <summary>
        /// Get available services by Service Center ID
        /// </summary>
        [HttpGet("by-service-center/{serviceCenterId}")]
        public async Task<IActionResult> GetServicesByServiceCenter(int serviceCenterId)
        {
            var services = await _serviceOfferedRepository.GetServicesByServiceCenterIdAsync(serviceCenterId);
            if (services == null || services.Count == 0)
                return NotFound("No services found for this service center.");

            return Ok(services);
        }
    }
}
