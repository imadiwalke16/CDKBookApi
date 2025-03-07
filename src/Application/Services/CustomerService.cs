using System;
using Application.Interfaces;
//using Domain.Entities;
using Application.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _repository.GetAll();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _repository.Add(customer);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await _repository.Update(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            await _repository.Delete(id);
        }
    }
}
