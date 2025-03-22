using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Product.Application.Commands;
using Shopi.Product.Application.DTOs.Requests;

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

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand dto)
    {
        var product = await _mediator.Send(dto);
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
}