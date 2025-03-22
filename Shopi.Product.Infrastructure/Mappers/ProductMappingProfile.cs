using AutoMapper;
using Shopi.Product.Application.Commands;
using Shopi.Product.Application.DTOs.Requests;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries;
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
        });

        CreateMap<AppProduct, CreateProductCommand>();

        CreateMap<AppProduct, CreateProductResponseDto>();

        CreateMap<FilterProductsQuery, ProductsQuery>();
        CreateMap<UpdateProductDto, UpdateProductCommand>().ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));
        

        CreateMap<UpdateProductCommand, AppProduct>().ForMember(dest =>
            dest.Id, opt => opt.Ignore()).ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));
    }
}