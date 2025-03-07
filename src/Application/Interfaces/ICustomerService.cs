using System;
//using Domain.Entities;
using Application.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;
namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int id);
    }
}

