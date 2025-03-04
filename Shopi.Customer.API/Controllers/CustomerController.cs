using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopi.Core.Exceptions;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;
using Shopi.Customer.API.Repository;

namespace Shopi.Customer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CustomerController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        var customer = await _mediator.Send(_mapper.Map<CreateCustomerCommand>(dto));
        return Created(string.Empty, customer.Data);
    }
    
    [Authorize("CustomerRights")]
    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerDto dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = _mapper.Map<UpdateCustomerCommand>(dto);
        command.Id = Guid.Parse(userId);
        var customer = await _mediator.Send(command);
        
        return Ok(customer.Data);
    }

    [Authorize("CustomerRights")]
    [HttpGet("get-customer")]
    public async Task<IActionResult> GetUser([FromQuery] FilterCustomerQuery dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        dto.Id = Guid.Parse(userId);
        var customer = await _mediator.Send(dto);
        return Ok(customer.Data);
    }
}