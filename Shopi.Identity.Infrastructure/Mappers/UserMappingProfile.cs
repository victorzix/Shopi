using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shopi.Identity.Application.Commands;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Domain.Entities;

namespace Shopi.Identity.Infrastructure.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserDto, CreateUserCommand>();
        CreateMap<CreateUserCommand, RegisterUser>();
        CreateMap<CreateUserCommand, CreateUserDto>();
        CreateMap<CreateUserCommand, CreateCustomerDto>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<CreateUserCommand, CreateAdminDto>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<LoginUserDto, LoginUserCommand>();
        CreateMap<LoginUserCommand, LoginUserDto>();
        CreateMap<LoginUserCommand, LoginUser>();
        CreateMap<UpdateUserDto, IdentityUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(
                o =>
                    o.Condition((src, dest, value) => value != null));
        CreateMap<UpdateUserDto, UpdateUserCommand>();
        CreateMap<UpdateUserCommand, UpdateUserDto>();
    }
}