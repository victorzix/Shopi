using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopi.Admin.Application.Commands;
using Shopi.Admin.Application.DTOs;
using Shopi.Admin.Application.Queries;

namespace Shopi.Admin.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AdminController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateAdminDto dto)
    {
        var admin = await _mediator.Send(_mapper.Map<CreateAdminCommand>(dto));
        return Created(string.Empty, admin.Data);
    }

    [Authorize("ElevatedRights")]
    [HttpPatch("update/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateAdminDto dto, string id)
    {
        var command = _mapper.Map<UpdateAdminCommand>(dto);
        command.Id = Guid.Parse(id);
        var admin = await _mediator.Send(command);

        return Ok(admin.Data);
    }

    [Authorize("ElevatedRights")]
    [HttpGet("get-admin")]
    public async Task<IActionResult> GetUser([FromQuery] FilterAdminQuery? dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        dto.Id = Guid.Parse(userId);
        var admin = await _mediator.Send(dto);
        return Ok(admin.Data);
    }
}