﻿using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.Interfaces;

public interface ICategoryReadRepository
{
    Task<Category?> Get(Guid id);
    Task<IReadOnlyCollection<Category>> FilterCategories(FilterCategoriesQuery query);
    Task<List<Category>> GetMany(List<Guid> categoryIds);
    Task<int> GetCount(FilterCategoriesQuery query);
}