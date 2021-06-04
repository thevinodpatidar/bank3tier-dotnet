using AutoMapper;
using Bank3Tier.Api.Resources.Auth;
using Bank3Tier.Api.Resources.Transaction;
using Bank3Tier.Api.Resources.User;
using Bank3Tier.Core.Models;

namespace Bank3Tier.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<User, UserResource>();
            CreateMap<Transaction, TransactionResource>();

            // Resource to Domain
            CreateMap<UserResource, User>();
            CreateMap<TransactionResource, Transaction>();

            CreateMap<SaveUserResource, User>();
            CreateMap<CreateTransactionResource, Transaction>();

            CreateMap<User, LoginUserResponseResource>();
            CreateMap<Transaction, SuccessTransactionResource>()
                .ForMember(d => d.TotalBalance, map => map.MapFrom(s => s.User.Balance))
                .ForMember(d => d.Status, map => map.MapFrom(s => "SUCCESS"));
        }
    }
}
