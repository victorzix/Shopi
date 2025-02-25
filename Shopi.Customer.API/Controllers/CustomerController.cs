using AutoMapper;
using MediatR;
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

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerDto dto)
    {
        var customer = await _mediator.Send(_mapper.Map<UpdateCustomerCommand>(dto));
        return Ok(customer.Data);
    }

    [HttpGet("get-by-email-or-document")]
    public async Task<IActionResult> GetByEmailOrDocument([FromQuery] GetByEmailOrDocumentQuery dto)
    {
        var customer = await _mediator.Send(dto);
        return Ok(customer.Data);
    }

    // [HttpGet("{id}")]
    // public IActionResult GetUserById([FromRoute] Guid id)
    // {
    //     var customer = _repository.GetById(id);
    //     return Ok(customer);
    // }
    //
    // [HttpPatch]
    // public IActionResult Update([FromBody] UpdateCustomerDto dto)
    // {
    //     var customerToUpdate = _repository.GetById(Guid.Parse("96265e8e-6f76-4355-a13c-57225d0a6353"));
    //     var mappedDto = _mapper.Map<UpdateCustomerDto, AppCustomer>(dto, customerToUpdate);
    //     var customer = _repository.Update(mappedDto);
    //     return Ok(customer);
    // }
}