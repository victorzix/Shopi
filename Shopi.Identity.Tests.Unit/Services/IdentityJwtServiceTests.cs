using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Identity.Domain.Entities;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.API.Services;
using Shopi.Identity.Application.Interfaces;
using Shopi.Identity.Infrastructure.Services;

namespace Shopi.Identity.API.UnitTests.Services;

public class IdentityJwtServiceTests
{
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<SignInManager<IdentityUser>> _signInManagerMock;
    private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private readonly IdentityJwtService _identityJwtService;
    private readonly IMapper _mapper;
    private readonly Mock<IEmailService> _emailService;

    public IdentityJwtServiceTests()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

        _emailService = new Mock<IEmailService>();
        _signInManagerMock = new Mock<SignInManager<IdentityUser>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
            null, null, null, null);

        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
            Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

        var jwtSettings = Options.Create(new JwtSettings()
        {
            Secret = Guid.NewGuid().ToString(),
            ExpireTime = 1,
            Emitter = "shopi",
            Audit = "shopi"
        });

        _mapper = new Mock<IMapper>().Object;

        _identityJwtService = new IdentityJwtService(
            _userManagerMock.Object, jwtSettings, _signInManagerMock.Object, _roleManagerMock.Object, _mapper, _emailService.Object);
    }

    [Fact]
    public async Task Register_ShouldReturnError_WhenEmailAlreadyExists()
    {
        var registerUser = new RegisterUser() { Email = "test@email.com", Password = "123456", Role = "Admin" };
        _roleManagerMock
            .Setup(x => x.FindByNameAsync(registerUser.Role))
            .ReturnsAsync(new IdentityRole(registerUser.Role));
        _userManagerMock.Setup(x => x.FindByEmailAsync(registerUser.Email)).ReturnsAsync(new IdentityUser());

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.Register(registerUser));
        var errors = (List<string>)exception.Errors;
        Assert.Equal("Erro de validação", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Email já cadastrado", errors[0]);
    }

    [Fact]
    public async Task Register_ShouldReturnAnApiResponsesWithUserId_WhenSuccess()
    {
        var registerUser = new RegisterUser() { Email = "test@gmail.com", Password = "123456", Role = "Admin" };
        _roleManagerMock
            .Setup(x => x.FindByNameAsync(registerUser.Role))
            .ReturnsAsync(new IdentityRole(registerUser.Role));

        _userManagerMock.Setup(x =>
            x.CreateAsync(It.IsAny<IdentityUser>(), registerUser.Password)).ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(x =>
            x.AddToRoleAsync(It.IsAny<IdentityUser>(), registerUser.Role)).ReturnsAsync(IdentityResult.Success);

        var result = await _identityJwtService.Register(registerUser);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.IsType<Guid>(result.Data.UserId);
    }

    [Fact]
    public async Task Register_ShouldReturnError_WhenRoleNotFound()
    {
        var registerUser = new RegisterUser() { Email = "test@email.com", Password = "123456", Role = "Admin" };

        _roleManagerMock
            .Setup(x => x.FindByNameAsync(registerUser.Role))
            .ReturnsAsync((IdentityRole)null);

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.Register(registerUser));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro ao realizar o cadastro", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Role informada não encontrada", errors[0]);
    }

    [Fact]
    public async Task Register_ShouldReturnError_WhenValidationFails()
    {
        var registerUser = new RegisterUser() { Email = "test", Password = "123", Role = "Admin" };

        _roleManagerMock
            .Setup(x => x.FindByNameAsync(registerUser.Role))
            .ReturnsAsync(new IdentityRole(registerUser.Role));

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.Register(registerUser));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro de validação", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Email inválido", errors[0]);
    }

    [Fact]
    public async Task Register_ShouldReturnError_WhenCreateAsyncFail()
    {
        var registerUser = new RegisterUser() { Email = "test@email.com", Password = "123456", Role = "Admin" };
        _roleManagerMock
            .Setup(x => x.FindByNameAsync(registerUser.Role))
            .ReturnsAsync(new IdentityRole(registerUser.Role));


        var identityErrors = new List<IdentityError>
        {
            new IdentityError { Code = "UserCreationFailed", Description = "Falha inesperada ao criar usuário" }
        };

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), registerUser.Password))
            .ReturnsAsync(IdentityResult.Failed(identityErrors.ToArray()));

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.Register(registerUser));
        var errors = (List<string>)exception.Errors;
        Assert.Equal("Erro ao realizar o cadastro", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Falha inesperada ao criar usuário", errors[0]);
    }

    [Fact]
    public async Task Login_ShouldReturnError_WhenValidationFails()
    {
        LoginUser loginUser = new LoginUser { Email = "test", Password = "1234" };

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.Login(loginUser));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro de validação", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Email inválido", errors[0]);
    }

    [Fact]
    public async Task Login_ShouldReturnError_WhenUserIsNotFound()
    {
        LoginUser loginUser = new LoginUser { Email = "test@test.com", Password = "1234" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(loginUser.Email)).ReturnsAsync((IdentityUser)null);

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.Login(loginUser));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro ao realizar login", exception.Message);
        Assert.Equal(401, exception.StatusCode);
        Assert.Contains("Usuário ou senha inválidos", errors[0]);
    }

    [Fact]
    public async Task Login_ShouldReturnError_WhenSignInAsyncFails()
    {
        LoginUser loginUser = new LoginUser { Email = "test@test.com", Password = "1234" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(loginUser.Email)).ReturnsAsync(new IdentityUser());

        _signInManagerMock.Setup(x => x.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true))
            .ReturnsAsync(SignInResult.Failed);

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.Login(loginUser));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro ao realizar login", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Usuário ou senha inválidos", errors[0]);
    }

    [Fact]
    public async Task Login_ShouldReturnAnApiResponsesWithWithJwtToken_WhenSuccess()
    {
        LoginUser loginUser = new LoginUser { Email = "test@test.com", Password = "1234" };
        var identityUser = new IdentityUser
            { Id = Guid.NewGuid().ToString(), Email = loginUser.Email, UserName = loginUser.Email };

        _userManagerMock.Setup(x => x.FindByEmailAsync(loginUser.Email))
            .ReturnsAsync(identityUser);
        _userManagerMock.Setup(x => x.GetRolesAsync(identityUser)).ReturnsAsync(new List<string> { "Customer" });
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true))
            .ReturnsAsync(SignInResult.Success);
        _userManagerMock.Setup(x => x.GetClaimsAsync(It.IsAny<IdentityUser>()))
            .ReturnsAsync(new List<Claim>());

        var result = await _identityJwtService.Login(loginUser);

        Assert.NotNull(result);
        Assert.IsType<string>(result.Data.Token);
    }

    [Fact]
    public async Task Delete_ShouldReturnAnError_WhenUserNotFound()
    {
        _userManagerMock.Setup(x => x.FindByIdAsync(Guid.NewGuid().ToString())).ReturnsAsync((IdentityUser)null);

        var exception =
            await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.DeleteUser(Guid.NewGuid()));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro de validação", exception.Message);
        Assert.Equal(404, exception.StatusCode);
        Assert.Contains("Usuário não encontrado", errors[0]);
    }

    [Fact]
    public async Task Delete_ShouldReturnVoid_WhenUserIsDeleted()
    {
        var userId = Guid.NewGuid().ToString();
        var user = new IdentityUser { Id = userId, UserName = "testuser" };

        _userManagerMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

        await _identityJwtService.DeleteUser(Guid.Parse(userId));
        _userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnAnError_WhenUserNotFound()
    {
        var dto = new UpdateUserDto { Email = "alo@alo.com" };
        _userManagerMock.Setup(x =>
            x.FindByIdAsync(Guid.NewGuid().ToString())).ReturnsAsync((IdentityUser)null);

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.UpdateUser(dto));
        var errors = (List<string>)exception.Errors;

        Assert.Equal(404, exception.StatusCode);
        Assert.Contains("Erro ao atualizar usuário", exception.Message);
        Assert.Equal("Usuário não encontrado", errors[0]);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnAnError_WhenNewEmailIsInUse()
    {
        var dto = new UpdateUserDto { Id = Guid.NewGuid(), Email = "alo@alo.com" };
        var user = new IdentityUser { Id = dto.Id.ToString(), Email = "dto@email.com" };

        _userManagerMock.Setup(x =>
            x.FindByIdAsync(dto.Id.ToString())).ReturnsAsync(user);

        _userManagerMock.Setup(x =>
            x.FindByEmailAsync(dto.Email)).ReturnsAsync(new IdentityUser
            { Id = Guid.NewGuid().ToString(), Email = "alo@alo.com" });

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.UpdateUser(dto));
        var errors = (List<string>)exception.Errors;

        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Erro ao atualizar usuário", exception.Message);
        Assert.Equal("Email já em uso", errors[0]);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnAnError_WhenUpdateAsyncFail()
    {
        var dto = new UpdateUserDto { Id = Guid.NewGuid(), Email = "alo@alo.com" };
        var user = new IdentityUser { Id = dto.Id.ToString(), Email = "dto@email.com" };

        _userManagerMock.Setup(x =>
            x.FindByIdAsync(dto.Id.ToString())).ReturnsAsync(user);

        _userManagerMock.Setup(x =>
            x.FindByEmailAsync(dto.Email)).ReturnsAsync((IdentityUser)null);

        var identityErrors = new List<IdentityError>
        {
            new IdentityError { Code = "UserUpdateFailed", Description = "Falha inesperada ao atualizar o usuário" }
        };

        _userManagerMock.Setup(x =>
            x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Failed(identityErrors.ToArray()));

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.UpdateUser(dto));
        var errors = (List<string>)exception.Errors;

        Assert.Equal(400, exception.StatusCode);
        Assert.Equal("Erro ao atualizar usuário", exception.Message);
        Assert.Contains("Falha", errors[0]);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnAnError_WhenResetPasswordAsyncFail()
    {
        var dto = new UpdateUserDto { Id = Guid.NewGuid(), Password = "1234ad" };
        var user = new IdentityUser { Id = dto.Id.ToString(), Email = "dto@email.com" };
        string token = "token1";

        _userManagerMock.Setup(x =>
            x.FindByIdAsync(dto.Id.ToString())).ReturnsAsync(user);

        _userManagerMock.Setup(x =>
            x.FindByEmailAsync(dto.Email)).ReturnsAsync((IdentityUser)null);


        var identityErrors = new List<IdentityError>
        {
            new IdentityError { Code = "PasswordResetFailed", Description = "Falha inesperada ao atualizar o usuário" }
        };

        _userManagerMock.Setup(x =>
            x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
        _userManagerMock.Setup(x => x.ResetPasswordAsync(user, token, dto.Password))
            .ReturnsAsync(IdentityResult.Failed(identityErrors.ToArray()));

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _identityJwtService.UpdateUser(dto));
        var errors = (List<string>)exception.Errors;

        Assert.Equal(400, exception.StatusCode);
        Assert.Equal("Erro ao atualizar usuário", exception.Message);
        Assert.Contains("Falha", errors[0]);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnVoid_WhenUpdateSucceed()
    {
        var dto = new UpdateUserDto { Id = Guid.NewGuid(), Email = "alo@alo.com", Password = "1234" };
        var user = new IdentityUser { Id = dto.Id.ToString(), Email = "dto@email.com" };

        _userManagerMock.Setup(x =>
            x.FindByIdAsync(dto.Id.ToString())).ReturnsAsync(user);

        _userManagerMock.Setup(x =>
            x.FindByEmailAsync(dto.Email)).ReturnsAsync((IdentityUser)null);

        _userManagerMock.Setup(x =>
            x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync("token");
        _userManagerMock.Setup(x => x.ResetPasswordAsync(user, "token", dto.Password))
            .ReturnsAsync(IdentityResult.Success);

        await _identityJwtService.UpdateUser(dto);

        _userManagerMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }
}