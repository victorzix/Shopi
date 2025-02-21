using AutoMapper;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Mappers;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateCustomerDto, CreateCustomerCommand>();
        CreateMap<CreateCustomerDto, AppCustomer>();
        CreateMap<CreateCustomerCommand, AppCustomer>();
        CreateMap<UpdateCustomerDto, AppCustomer>().ForAllMembers(o =>
            o.Condition((src, dest, value) => value != null));
        CreateMap<AppCustomer, CreateCustomerResponseDto>();
    }
}