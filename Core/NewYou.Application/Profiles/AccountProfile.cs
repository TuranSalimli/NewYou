using AutoMapper;
using NewYou.Application.Features.Commands.Auth.Register;
using NewYou.Domain.Entities;

namespace NewYou.Application.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<RegisterCommand, Account>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}