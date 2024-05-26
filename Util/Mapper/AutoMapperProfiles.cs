using AutoMapper;
using BankAccountAPI.DTOs.Account;
using BankAccountAPI.DTOs.Customer;
using BankAccountAPI.DTOs.Transaction;
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
            CreateMap<Account, AccountWithTransactionDTO>()
                    .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions));

            CreateMap<CreateAccountDTO, Account>();

            CreateMap<Transaction, TransactionDTO>()
                .ForMember(dest => dest.ToAccount, opt => opt.MapFrom<TransferenceTransactionResolver>());
        }

        public class TransferenceTransactionResolver : IValueResolver<Transaction, TransactionDTO, AccountTransferenceDTO>
        {
            public AccountTransferenceDTO Resolve(Transaction source, TransactionDTO destination, AccountTransferenceDTO destMember, ResolutionContext context)
            {
                if (source.ToAccountId == null)
                {
                    return null;
                }
                return new AccountTransferenceDTO
                {
                    Id = (int)source.ToAccountId,
                    AccountNumber = source.ToAccount?.AccountNumber
                };
            }
        }

    }
}
