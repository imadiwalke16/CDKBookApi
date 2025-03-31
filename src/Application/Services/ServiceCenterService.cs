using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class ServiceCenterService : IServiceCenterService
    {
        private readonly IServiceCenterRepository _serviceCenterRepository;

        public ServiceCenterService(IServiceCenterRepository serviceCenterRepository)
        {
            _serviceCenterRepository = serviceCenterRepository;
        }

        public async Task<List<ServiceCenter>> GetServiceCentersByPinCodeAsync(string pinCode)
        {
            return await _serviceCenterRepository.GetByPinCodeAsync(pinCode);
        }
    }
}
