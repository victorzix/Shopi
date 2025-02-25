using AutoMapper;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Mappers;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<CreateAddressDto, CreateAddressCommand>();
        CreateMap<CreateAddressCommand, CreateAddressDto>();
        CreateMap<CreateAddressCommand, Address>();
        CreateMap<Address, CreateAddressResponse>();
        CreateMap<UpdateAddressDto, UpdateAddressCommand>();
        CreateMap<UpdateAddressCommand, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForAllMembers(
                o =>
                    o.Condition((src, dest, value) => value != null));
        
    }
}