using AutoMapper;
using Shopi.Product.Application.Commands;
using Shopi.Product.Application.DTOs;
using Shopi.Product.Application.Queries;
using Shopi.Product.Domain.Entities;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.Infrastructure.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductCommand, AppProduct>();
        CreateMap<AppProduct, CreateProductCommand>();

        CreateMap<AppProduct, CreateProductResponseDto>();
        
        CreateMap<FilterProductsQuery, ProductsQuery>();

    }
}