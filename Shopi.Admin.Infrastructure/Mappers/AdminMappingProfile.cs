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
        CreateMap<CreateAdminCommand, AppAdmin>().BeforeMap((s, d) =>
        {
            d.CreatedAt = DateTime.Now;
            d.UpdatedAt = DateTime.Now;
        });
        CreateMap<UpdateAdminDto, UpdateAdminCommand>();
        CreateMap<UpdateAdminCommand, UpdateUserDto>().ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));
        ;
        CreateMap<UpdateAdminCommand, AppAdmin>()
            .BeforeMap((s, d) => { d.UpdatedAt = DateTime.Now; })
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(
                o =>
                    o.Condition((src, dest, value) => value != null));
        CreateMap<AppAdmin, CreateAdminResponseDto>();
        CreateMap<FilterAdminQuery, QueryAdmin>();
    }
}