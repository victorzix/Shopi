using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.Core.Services;
using Shopi.Core.Utils;
using Shopi.Identity.API.CommandHandlers;
using Shopi.Identity.API.Commands;
using Shopi.Identity.API.DTOs;

namespace Shopi.Identity.API.Controllers;

[Route("Auth")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public IdentityController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register-customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<CreateUserCommand>(dto));
        return Created(string.Empty, user.Data);
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<UpdateUserCommand>(dto));
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginCustomer([FromBody] LoginUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<LoginUserCommand>(dto));
        if (!user.Success)
        {
            return BadRequest(user.Errors);
        }

        return Ok(user.Data.Token);
    }
}