using System;
//using Domain.Entities;
using Application.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;
namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Delete(int id);
    }
}

