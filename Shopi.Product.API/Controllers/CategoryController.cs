using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Product.Application.Commands;
using Shopi.Product.Application.DTOs.Requests;
using Shopi.Product.Application.Queries;


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

    [HttpGet("get-category/{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryQuery(id));
        return Ok(category.Data);
    }
    
    [HttpGet("filter")]
    public async Task<IActionResult> FilterCategories([FromQuery] FilterCategoriesDto query)
    {
        var categories = await _mediator.Send(_mapper.Map<FilterCategoriesQuery>(query));
        return Ok(categories.Data);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        var category = await _mediator.Send(_mapper.Map<CreateCategoryCommand>(dto));
        return Created(string.Empty, category.Data);
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        var updateCategory = _mapper.Map<UpdateCategoryCommand>(dto);
        updateCategory.Id = id;
        var category = await _mediator.Send(updateCategory);
        return Ok(category.Data);
    }

    [HttpPatch("change-visibility/{id}")]
    public async Task<IActionResult> ChangeCategoryVisibility(Guid id)
    {
        await _mediator.Send(new ChangeVisibilityCommand(id));
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _mediator.Send(new DeleteCommand(id));
        return NoContent();
    }
}