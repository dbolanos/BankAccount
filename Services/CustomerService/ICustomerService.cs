using BankAccountAPI.DTOs.Customer;

namespace BankAccountAPI.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO customerDTO);
        Task<CustomerDTO> GetCustomerByIdAsync(int id);

        Task<List<CustomerDTO>> GetCustomersAsync();

        Task<List<CustomerDTO>> GetCustomerWithAccounts();
    }
}
