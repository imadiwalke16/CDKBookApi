using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceCenterService
    {
        Task<List<ServiceCenter>> GetServiceCentersByPinCodeAsync(string pinCode);
    }
}
