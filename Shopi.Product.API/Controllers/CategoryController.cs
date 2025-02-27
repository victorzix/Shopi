using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Product.API.Commands;
using Shopi.Product.API.DTOs;

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
}