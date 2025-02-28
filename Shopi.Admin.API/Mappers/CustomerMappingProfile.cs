using AutoMapper;
using Shopi.Admin.API.Commands;
using Shopi.Admin.API.DTOs;
using Shopi.Admin.API.Models;

namespace Shopi.Admin.API.Mappers;

public class AdminMappingProfile : Profile
{
    public AdminMappingProfile()
    {
        CreateMap<CreateAdminDto, CreateAdminCommand>();
        CreateMap<CreateAdminDto, AppAdmin>();
        CreateMap<CreateAdminCommand, AppAdmin>();
        CreateMap<UpdateAdminDto, UpdateAdminCommand>();
        CreateMap<UpdateAdminCommand, UpdateUserDto>().ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));;
        CreateMap<UpdateAdminCommand, AppAdmin>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(
                o =>
                    o.Condition((src, dest, value) => value != null));
        CreateMap<AppAdmin, CreateAdminResponseDto>();
    }
}