using AutoMapper;
using Shopi.Product.Application.Commands.CategoriesCommands;
using Shopi.Product.Application.DTOs.Requests;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries.CategoriesQueries;
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
            d.CreatedAt = DateTime.Now;
            d.UpdatedAt = DateTime.Now;
        });
        CreateMap<CreateCategoryDto, CreateCategoryCommand>();
        CreateMap<Category, CategoryResponseDto>();

        CreateMap<UpdateCategoryCommand, Category>()
            .BeforeMap((s, d) => { d.UpdatedAt = DateTime.Now; })
            .ForMember(dest =>
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