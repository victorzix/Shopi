using Microsoft.AspNetCore.Identity;
using Shopi.Core.Utils;
using Shopi.Identity.API.DTOs;
using Shopi.Identity.API.Models;

namespace Shopi.Identity.API.Interfaces;

public interface IIdentityJwtService
{
    Task<ApiResponses<RegisterUserResponseDto>> Register(RegisterUser registerUser);
    Task<ApiResponses<LoginUserResponseDto>> Login(LoginUser loginUser);
    Task DeleteUser(Guid id);
    Task UpdateUser(UpdateUserDto dto);
}