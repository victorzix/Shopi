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
    }
}