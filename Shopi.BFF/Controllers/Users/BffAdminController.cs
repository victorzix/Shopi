using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.BFF.DTOs;
using Shopi.BFF.DTOs.Admin;
using Shopi.BFF.DTOs.User;
using Shopi.Core.Interfaces;
using Shopi.Core.Services;
using Shopi.Core.Utils;

namespace Shopi.BFF.Controllers.Users;

[ApiController]
[Route("/api/v1/admin")]
[Tags("Admins")]
public class BffAdminController : ControllerBase
{
    private readonly IBffHttpClient _httpClient;

    public BffAdminController(IBffHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var adminResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.AuthApiUrl, "register-admin", dto);
        if (!adminResponse.IsSuccessStatusCode)
        {
            var errorContent = await adminResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await adminResponse.Content.ReadAsStringAsync();
        return Ok(new { Token = content });
    }

    [HttpGet("get-data")]
    public async Task<IActionResult> Get()
    {
        var authorizationHeader = Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return Unauthorized(new { message = "Token não fornecido ou formato inválido." });
        }

        var headers = new Dictionary<string, string>
        {
            { "Authorization", authorizationHeader }
        };
        var adminResponse = await _httpClient.Get<object>(MicroServicesUrls.AdminApiUrl, "get-admin", headers: headers);
        
        if (!adminResponse.IsSuccessStatusCode)
        {
            var errorContent = await adminResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await adminResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<GetAdminResponseDto>(content);
        await Task.Delay(5000);
        return Ok(deserializedContent);
    }
}