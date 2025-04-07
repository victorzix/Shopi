using AutoMapper;
using Shopi.Customer.Application.Commands;
using Shopi.Customer.Application.DTOs;
using Shopi.Customer.Application.Queries;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.Infrastructure.Mappers;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateCustomerDto, CreateCustomerCommand>();
        CreateMap<CreateCustomerCommand, AppCustomer>().BeforeMap((s, d) =>
        {
            d.IsActive = true;
            d.CreatedAt = DateTime.Now.ToUniversalTime();
            d.UpdatedAt = DateTime.Now.ToUniversalTime();
        });
        CreateMap<UpdateCustomerDto, UpdateCustomerCommand>();
        CreateMap<UpdateCustomerCommand, UpdateUserDto>().ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));
        ;
        CreateMap<UpdateCustomerCommand, AppCustomer>().BeforeMap((s, d) => { d.UpdatedAt = DateTime.Now.ToUniversalTime(); })
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(
                o =>
                    o.Condition((src, dest, value) => value != null));
        CreateMap<AppCustomer, CreateCustomerResponseDto>();
        CreateMap<FilterCustomerQuery, QueryCustomer>();
    }
}