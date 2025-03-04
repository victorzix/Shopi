using AutoMapper;
using Shopi.Product.API.Commands;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.Mappers;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<CreateCategoryDto, CreateCategoryCommand>();
        CreateMap<Category, CreateCategoryResponseDto>();

        CreateMap<UpdateCategoryCommand, Category>().ForMember(dest =>
            dest.Id, opt => opt.Ignore()).ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));
        ;
        CreateMap<UpdateCategoryDto, UpdateCategoryCommand>().ForAllMembers(
            o =>
                o.Condition((src, dest, value) => value != null));
        ;

        CreateMap<FilterCategoriesDto, FilterCategoriesQuery>();
    }
}