using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.BFF.Models;
using Shopi.Core.Services;
using Shopi.Core.Utils;
using Shopi.Identity.API.DTOs;

namespace Shopi.BFF.Controllers;

[ApiController]
[Route("/api/v1/customer")]
public class BffClientController : ControllerBase
{
    private readonly BffHttpClient _httpClient;

    public BffClientController(BffHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var userResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.AuthApiUrl, "register-client", dto);
        var content = await userResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject(content);
        return Ok(deserializedContent);
    }
    }