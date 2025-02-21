using AutoMapper;
using Shopi.Identity.API.Commands;
using Shopi.Identity.API.DTOs;
using Shopi.Identity.API.Models;

namespace Shopi.Identity.API.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserDto, CreateUserCommand>();
        CreateMap<CreateUserCommand, RegisterUser>();
        CreateMap<CreateUserCommand, CreateUserDto>();
        CreateMap<CreateUserCommand, CreateCustomerDto>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<LoginUserDto, LoginUserCommand>();
        CreateMap<LoginUserCommand, LoginUserDto>();
        CreateMap<LoginUserCommand, LoginUser>();
    }
}