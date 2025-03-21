using AutoMapper;
using Shopi.Customer.Application.Commands;
using Shopi.Customer.Application.DTOs;
using Shopi.Customer.Application.Queries;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.Infrastructure.Mappers;

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

        CreateMap<GetAddressQuery, QueryAddress>();
        CreateMap<ListAddressesQuery, QueryAddresses>();
    }
}