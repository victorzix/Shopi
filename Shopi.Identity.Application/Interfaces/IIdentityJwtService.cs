using Shopi.Core.Utils;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Domain.Entities;

namespace Shopi.Identity.Application.Interfaces;

public interface IIdentityJwtService
{
    Task<ApiResponses<RegisterUserResponseDto>> Register(RegisterUser registerUser);
    Task<ApiResponses<LoginUserResponseDto>> Login(LoginUser loginUser);
    Task DeleteUser(Guid id);
    Task<ApiResponses<LoginUserResponseDto>> ConfirmEmail(string token);
    Task UpdateUser(UpdateUserDto dto);
    Task<string> ResendConfirmationEmail(string email);
}