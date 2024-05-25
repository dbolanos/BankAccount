﻿using AutoMapper;
using BankAccountAPI.DTOs.Customer;
using BankAccountAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public CustomerService(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        public async Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO customerDTO)
        {
            var customer = mapper.Map<Customer>(customerDTO);
            context.Add(customer);
            await context.SaveChangesAsync();

            return mapper.Map<CustomerDTO>(customer);
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                throw new Exception("Cuenta no encontrada.");
            }

            return mapper.Map<CustomerDTO>(customer);

        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await context.Customers.ToListAsync();
            return mapper.Map<List<CustomerDTO>>(customers);
        }

        public Task<List<CustomerDTO>> GetCustomerWithAccounts()
        {
            throw new NotImplementedException();
        }
    }
}