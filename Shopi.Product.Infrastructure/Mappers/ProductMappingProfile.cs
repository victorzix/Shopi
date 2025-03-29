using AutoMapper;
using Shopi.Product.Application.Commands.ProductsCommands;
using Shopi.Product.Application.DTOs.Requests;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries.ProductsQueries;
using Shopi.Product.Domain.Entities;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.Infrastructure.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductCommand, AppProduct>().BeforeMap((s, d) =>
        {
            d.IsActive = true;
            d.Visible = true;
            d.CreatedAt = DateTime.Now;
            d.UpdatedAt = DateTime.Now;
        });

        CreateMap<AppProduct, CreateProductCommand>();

        CreateMap<AppProduct, ProductResponseDto>();

        CreateMap<FilterProductsDto, FilterProductsQuery>();

        CreateMap<FilterProductsQuery, ProductsQuery>();
        CreateMap<UpdateProductDto, UpdateProductCommand>().ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));

        CreateMap<CreateProductDto, CreateProductCommand>();
        CreateMap<AppProduct, ProductResponseDto>();

        CreateMap<UpdateProductCommand, AppProduct>()
            .BeforeMap((s, d) => { d.UpdatedAt = DateTime.Now; })
            .ForMember(dest =>
                dest.Id, opt => opt.Ignore()).ForAllMembers(
                o =>
                    o.Condition((src, dest, value) => value != null));

        CreateMap<FilterProductsDto, FilterProductsQuery>();
        CreateMap<FilterProductsQuery, ProductsQuery>();
    }
}