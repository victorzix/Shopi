using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AddressController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [Authorize("CustomerRights")]
    [HttpPost("add-address")]
    public async Task<IActionResult> AddAddress([FromBody] CreateAddressDto dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = _mapper.Map<CreateAddressCommand>(dto);
        command.CustomerId = Guid.Parse(userId);
        var address = await _mediator.Send(command);
        return Created(string.Empty, address);
    }

    [Authorize("CustomerRights")]
    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressDto dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = _mapper.Map<UpdateAddressCommand>(dto);
        command.CustomerId = Guid.Parse(userId);
        command.Id = id;

        var address = await _mediator.Send(command);
        return Ok(address.Data);
    }

    [Authorize("CustomerRights")]
    [HttpGet("list-address")]
    public async Task<IActionResult> ListAddresses()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var address = await _mediator.Send(new ListAddressesQuery(Guid.Parse(userId)));
        return Ok(address.Data);
    }


    [Authorize("CustomerRights")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddress(Guid id)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var address = await _mediator.Send(new GetAddressQuery(id, Guid.Parse(userId)));
        return Ok(address);
    }
}