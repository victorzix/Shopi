using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.BFF.DTOs.User;
using Shopi.Core.Services;
using Shopi.Core.Utils;

namespace Shopi.BFF.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class BffAuthController : ControllerBase
{
    private readonly BffHttpClient _httpClient;

    public BffAuthController(BffHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Create([FromBody] LoginUserDto dto)
    {
        var userResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.AuthApiUrl, "login", dto);
        if (!userResponse.IsSuccessStatusCode)
        {
            var errorContent = await userResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await userResponse.Content.ReadAsStringAsync();
        return Ok(new { Token = content });
    }
}