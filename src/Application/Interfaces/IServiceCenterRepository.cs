using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceCenterRepository
    {
        Task<List<ServiceCenter>> GetByPinCodeAsync(string pinCode);
    }
}
