using AutoMapper;
using Shopi.Product.API.Commands;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductCommand, AppProduct>();
        CreateMap<AppProduct, CreateProductCommand>();

        CreateMap<AppProduct, CreateProductResponseDto>();
    }
}