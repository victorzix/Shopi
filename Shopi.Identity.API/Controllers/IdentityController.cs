using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Core.Exceptions;
using Shopi.Identity.Application.Commands;
using Shopi.Identity.Application.DTOs;

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


    [HttpPost("register-admin")]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<CreateUserCommand>(dto));
        return Created(string.Empty, user.Data);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        var accessToken = await _mediator.Send(new ConfirmEmailCommand(token));
        return Ok(accessToken.Data.Token);
    }
    
    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendEmailConfirmation([FromQuery] string token)
    {
        await _mediator.Send(new ResendEmailConfirmationCommand(token));
        return NoContent();
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<UpdateUserCommand>(dto));
        return NoContent();
    }

    [HttpPost("login-customer")]
    public async Task<IActionResult> LoginCustomer([FromBody] LoginUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<LoginUserCommand>(dto));
        if (user.Data.Role == "Administrator")
            throw new CustomApiException("Erro ao realizar Login", StatusCodes.Status403Forbidden,
                "Administradores não podem acessar esta área");
        if (!user.Success)
        {
            return BadRequest(user.Errors);
        }

        return Ok(user.Data.Token);
    }

    [HttpPost("login-admin")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<LoginUserCommand>(dto));
        if (user.Data.Role == "Customer")
            throw new CustomApiException("Erro ao realizar Login", StatusCodes.Status403Forbidden,
                "Clientes não podem acessar esta área");
        if (!user.Success)
        {
            return BadRequest(user.Errors);
        }

        return Ok(user.Data.Token);
    }
}