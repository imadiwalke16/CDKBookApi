using System;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/service-centers")]
    public class ServiceCenterController : ControllerBase
    {
        private readonly IServiceCenterService _serviceCenterService;

        public ServiceCenterController(IServiceCenterService serviceCenterService)
        {
            _serviceCenterService = serviceCenterService;
        }

        /// <summary>
        /// 🔹 Get service centers by pin code
        /// </summary>
        [HttpGet("by-pin/{pinCode}")]
        public async Task<IActionResult> GetByPinCode(string pinCode)
        {
            var serviceCenters = await _serviceCenterService.GetServiceCentersByPinCodeAsync(pinCode);
            if (serviceCenters == null || serviceCenters.Count == 0)
            {
                return NotFound("No service centers found for this pin code.");
            }
            return Ok(serviceCenters);



        }
    }
}


