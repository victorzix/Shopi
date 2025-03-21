using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Application.Validators;
using Shopi.Identity.Domain.Entities;
using Shopi.Identity.Infrastructure.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Shopi.Identity.API.Services;

public class IdentityJwtService : IIdentityJwtService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public IdentityJwtService(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings,
        SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<ApiResponses<RegisterUserResponseDto>> Register(RegisterUser registerUser)
    {
        var roleExists = await _roleManager.FindByNameAsync(registerUser.Role);
        if (roleExists == null)
        {
            throw new CustomApiException("Erro ao realizar o cadastro", StatusCodes.Status400BadRequest,
                "Role informada não encontrada");
        }

        var userExists = await _userManager.FindByEmailAsync(registerUser.Email);
        if (userExists != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest, "Email já cadastrado");
        }

        var validator = new RegisterUserValidator();
        var validateRegisterUser = validator.Validate(registerUser);
        if (!validateRegisterUser.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validateRegisterUser.Errors.Select(e => e.ErrorMessage));
        }

        var user = new IdentityUser
        {
            Email = registerUser.Email,
            UserName = registerUser.Email
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (!result.Succeeded)
        {
            throw new CustomApiException("Erro ao realizar o cadastro", StatusCodes.Status400BadRequest,
                result.Errors.Select(e => e.Description).ToList());
        }

        await _userManager.AddToRoleAsync(user, registerUser.Role);

        return new ApiResponses<RegisterUserResponseDto>
        {
            Data = new RegisterUserResponseDto { UserId = Guid.Parse(user.Id) },
            Success = true
        };
    }

    public async Task<ApiResponses<LoginUserResponseDto>> Login(LoginUser loginUser)
    {
        var validator = new LoginUserValidator();
        var validateRegisterUser = validator.Validate(loginUser);
        if (!validateRegisterUser.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validateRegisterUser.Errors.Select(e => e.ErrorMessage));
        }

        var user = await _userManager.FindByEmailAsync(loginUser.Email);
        if (user == null)
        {
            throw new CustomApiException("Erro ao realizar login", StatusCodes.Status401Unauthorized,
                "Usuário ou senha inválidos");
        }

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
        if (!result.Succeeded)
        {
            throw new CustomApiException("Erro ao realizar login", StatusCodes.Status400BadRequest,
                "Usuário ou senha inválidos");
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);

        return new ApiResponses<LoginUserResponseDto>
            { Data = new LoginUserResponseDto { Token = await GenerateJwt(user), Role = userRoles.FirstOrDefault() } };
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Usuário não encontrado");
        }
        await _userManager.DeleteAsync(user);
    }

    public async Task UpdateUser(UpdateUserDto dto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(dto.Id.ToString());
        if (userToUpdate == null)
        {
            throw new CustomApiException("Erro ao atualizar usuário", StatusCodes.Status404NotFound,
                "Usuário não encontrado");
        }

        if (!string.IsNullOrEmpty(dto.Email) && dto.Email != userToUpdate.Email)
        {
            var emailInUse = await _userManager.FindByEmailAsync(dto.Email);
            if (emailInUse != null)
            {
                throw new CustomApiException("Erro ao atualizar usuário", StatusCodes.Status400BadRequest,
                    "Email já em uso");
            }
        }

        _mapper.Map(dto, userToUpdate);

        var updateResult = await _userManager.UpdateAsync(userToUpdate);
        if (!updateResult.Succeeded)
        {
            throw new CustomApiException("Erro ao atualizar usuário", StatusCodes.Status400BadRequest,
                updateResult.Errors.Select(e => e.Description));
        }

        if (!string.IsNullOrEmpty(dto.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(userToUpdate);
            var passwordResult = await _userManager.ResetPasswordAsync(userToUpdate, token, dto.Password);

            if (!passwordResult.Succeeded)
            {
                throw new CustomApiException("Erro ao atualizar usuário", StatusCodes.Status400BadRequest,
                    passwordResult.Errors.Select(e => e.Description));
            }
        }
    }

    private async Task<string> GenerateJwt(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.AddDays(7).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.AddDays(7).ToString(),
            ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Emitter,
            Audience = _jwtSettings.Audit,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpireTime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }
}