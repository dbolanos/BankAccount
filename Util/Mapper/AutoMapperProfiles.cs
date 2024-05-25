using AutoMapper;
using BankAccountAPI.DTOs.Account;
using BankAccountAPI.DTOs.Customer;
using BankAccountAPI.Entities;

namespace BankAccountAPI.Util.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<CreateCustomerDTO, Customer>();

            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<CreateAccountDTO, Account>();
        }

    }
}
