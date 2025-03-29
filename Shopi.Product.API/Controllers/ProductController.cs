using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Product.Application.Commands.ProductsCommands;
using Shopi.Product.Application.DTOs.Requests;
using Shopi.Product.Application.Queries.ProductsQueries;

namespace Shopi.Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("get-product/{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));
        return Ok(product.Data);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterProducts([FromQuery] FilterProductsDto query)
    {
        var products = await _mediator.Send(_mapper.Map<FilterProductsQuery>(query));
        return Ok(products.Data);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        var product = await _mediator.Send(_mapper.Map<CreateProductCommand>(dto));
        return Created(string.Empty, product.Data);
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto dto, Guid id)
    {
        var product = _mapper.Map<UpdateProductCommand>(dto);
        product.Id = id;
        var updatedProduct = await _mediator.Send(product);
        return Ok(updatedProduct.Data);
    }

    [HttpPatch("change-visibility/{id}")]
    public async Task<IActionResult> ChangeProductVisibility(Guid id)
    {
        await _mediator.Send(new ChangeProductVisibilityCommand { Id = id });
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }
}