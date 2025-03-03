using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.BFF.DTOs;
using Shopi.BFF.DTOs.User;
using Shopi.Core.Services;
using Shopi.Core.Utils;

namespace Shopi.BFF.Controllers.Users;

[ApiController]
[Route("/api/v1/admin")]
public class BffAdminController : ControllerBase
{
    private readonly BffHttpClient _httpClient;

    public BffAdminController(BffHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var userResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.AuthApiUrl, "register-admin", dto);
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