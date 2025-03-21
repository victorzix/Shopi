using Shopi.Core.Utils;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Domain.Entities;

namespace Shopi.Identity.Infrastructure.Interfaces;

public interface IIdentityJwtService
{
    Task<ApiResponses<RegisterUserResponseDto>> Register(RegisterUser registerUser);
    Task<ApiResponses<LoginUserResponseDto>> Login(LoginUser loginUser);
    Task DeleteUser(Guid id);
    Task UpdateUser(UpdateUserDto dto);
}