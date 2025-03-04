﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Product.API.Commands;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        var category = await _mediator.Send(_mapper.Map<CreateCategoryCommand>(dto));
        return Created(string.Empty, category.Data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterCategories([FromQuery] FilterCategoriesDto query)
    {
        var categories = await _mediator.Send(_mapper.Map<FilterCategoriesQuery>(query));
        return Ok(categories.Data);
    }
}