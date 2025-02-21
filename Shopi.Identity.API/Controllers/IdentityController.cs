using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.Core.Services;
using Shopi.Core.Utils;
using Shopi.Identity.API.Commands;
using Shopi.Identity.API.DTOs;

namespace Shopi.Identity.API.Controllers;

[Route("Auth")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly BffHttpClient _httpClient;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public IdentityController(BffHttpClient httpClient, IMediator mediator, IMapper mapper)
    {
        _httpClient = httpClient;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register-client")]
    public async Task<IActionResult> CreateClient([FromBody] CreateUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<CreateUserCommand>(dto));
        if (!user.Success)
        {
            return BadRequest(user.Errors);
        }


        return Ok(dto);
        // var clientResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.CustomerApiUrl, "", user.Data);
        // var content = await clientResponse.Content.ReadAsStringAsync();
        // var deserializedContent = JsonConvert.DeserializeObject<CreateClientDto>(content);
        //
        // return Created(string.Empty, deserializedContent);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginClient([FromBody] LoginUserDto dto)
    {
        var user = await _mediator.Send(_mapper.Map<LoginUserCommand>(dto));
        if (!user.Success)
        {
            Console.WriteLine(user.Errors);
            return BadRequest(user.Errors);
        }

        return Ok(user.Data.Token);
    }
}