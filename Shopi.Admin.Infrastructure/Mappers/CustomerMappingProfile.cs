using AutoMapper;
using Shopi.Admin.Application.Commands;
using Shopi.Admin.Application.DTOs;
using Shopi.Admin.Application.Queries;
using Shopi.Admin.Domain.Entities;
using Shopi.Admin.Domain.Queries;

namespace Shopi.Admin.Infrastructure.Mappers;

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
        CreateMap<FilterAdminQuery, QueryAdmin>();
    }
}