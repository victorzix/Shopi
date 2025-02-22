using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Identity.API.Models;
using Shopi.Identity.API.Services;

namespace Shopi.Identity.API.UnitTests.Services;

public class IdentityJwtServiceTests
{
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<SignInManager<IdentityUser>> _signInManagerMock;
    private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private readonly IdentityJwtService _identityJwtService;

    public IdentityJwtServiceTests()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

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

        _identityJwtService = new IdentityJwtService(
            _userManagerMock.Object, jwtSettings, _signInManagerMock.Object, _roleManagerMock.Object);
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
}