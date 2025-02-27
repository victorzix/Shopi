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
        CreateMap<FilterCategoriesDto, FilterCategoriesQuery>();
    }
}