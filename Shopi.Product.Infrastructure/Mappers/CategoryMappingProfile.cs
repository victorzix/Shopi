using AutoMapper;
using Shopi.Product.Application.Commands;
using Shopi.Product.Application.DTOs.Requests;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries;
using Shopi.Product.Domain.Entities;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.Infrastructure.Mappers;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CreateCategoryCommand, Category>().BeforeMap((s, d) =>
        {
            d.IsActive = true;
            d.Visible = true;
        });
        CreateMap<CreateCategoryDto, CreateCategoryCommand>();
        CreateMap<Category, CreateCategoryResponseDto>();

        CreateMap<UpdateCategoryCommand, Category>().ForMember(dest =>
            dest.Id, opt => opt.Ignore()).ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));

        CreateMap<UpdateCategoryDto, UpdateCategoryCommand>().ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));


        CreateMap<FilterCategoriesDto, FilterCategoriesQuery>();
        CreateMap<FilterCategoriesQuery, CategoriesQuery>();
    }
}