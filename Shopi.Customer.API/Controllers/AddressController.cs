using AutoMapper;
using MediatR;
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

    [HttpPost("{customerId}/add")]
    public async Task<IActionResult> AddAddress(Guid customerId, [FromBody] CreateAddressDto dto)
    {
        var command = _mapper.Map<CreateAddressCommand>(dto);
        command.CustomerId = customerId;
        var address = await _mediator.Send(command);
        return Created(string.Empty, address);
    }

    [HttpPatch("{customerId}/{id}")]
    public async Task<IActionResult> UpdateAddress(Guid customerId, Guid id, [FromBody] UpdateAddressDto dto)
    {
        var command = _mapper.Map<UpdateAddressCommand>(dto);
        command.CustomerId = customerId;
        command.Id = id;

        var address = await _mediator.Send(command);
        return Ok(address.Data);
    } 
    
    [HttpGet("{customerId}")]
    public async Task<IActionResult> ListAddresses(Guid customerId)
    {
        var address = await _mediator.Send(new ListAddressesQuery(customerId));
        return Ok(address);
    }
    
    
    [HttpGet("{customerId}/{id}")]
    public async Task<IActionResult> GetAddress(Guid customerId, Guid id)
    {
        var address = await _mediator.Send(new GetAddressQuery(id, customerId));
        return Ok(address);
    }
}