using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Product.Application.Commands;

namespace Shopi.Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController: ControllerBase
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
}