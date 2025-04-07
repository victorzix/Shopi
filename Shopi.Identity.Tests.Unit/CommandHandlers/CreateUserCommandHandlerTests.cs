using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using Shopi.Core.Exceptions;
using Shopi.Core.Interfaces;
using Shopi.Core.Utils;
using Shopi.Identity.API.CommandHandlers;
using Shopi.Identity.Application.Commands;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Domain.Entities;
using Shopi.Identity.Application.Interfaces;
using Shopi.Identity.Infrastructure.Mappers;

namespace Shopi.Identity.API.UnitTests.CommandHandlers;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IIdentityJwtService> _identityJwtServiceMock;
    private readonly IMapper _mapper;
    private readonly Mock<IBffHttpClient> _httpClientMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<UserMappingProfile>(); });

        _mapper = configuration.CreateMapper();
        _identityJwtServiceMock = new Mock<IIdentityJwtService>();
        _httpClientMock = new Mock<IBffHttpClient>();

        _handler = new CreateUserCommandHandler(
            _identityJwtServiceMock.Object,
            _mapper,
            _httpClientMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenJwtServiceFails()
    {
        var command = new CreateUserCommand("name", "email@email.com", "1234", "1234", "ts");
        _identityJwtServiceMock.Setup(x => x.Register(It.IsAny<RegisterUser>())).ThrowsAsync(
            new CustomApiException("Erro ao realizar o cadastro", StatusCodes.Status400BadRequest,
                "Role informada não encontrada"));

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _handler.Handle(command, default));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro ao realizar o cadastro", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Role informada não encontrada", errors[0]);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPostToCustomerApiFails()
    {
        var command = new CreateUserCommand("name", "email@email.com", "1234", "1234", "Customer");
        var registerUserDto = new RegisterUserResponseDto { UserId = Guid.NewGuid() };

        _identityJwtServiceMock.Setup(x =>
            x.Register(It.IsAny<RegisterUser>())).ReturnsAsync(new ApiResponses<RegisterUserResponseDto>
            { Data = registerUserDto });

        var errorModel = new ErrorModel
        {
            Title = "Erro ao realizar o cadastro", Status = StatusCodes.Status400BadRequest,
            Errors = new List<string> { "Documento já cadastrado" }
        };
        var errorContent = new StringContent(JsonConvert.SerializeObject(errorModel), System.Text.Encoding.UTF8,
            "application/json");

        _httpClientMock.Setup(x =>
                x.PostJsonAsync(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<CreateCustomerDto>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = errorContent
            });

        var exception = await Assert.ThrowsAsync<CustomApiException>(() => _handler.Handle(command, default));
        var errors = (List<string>)exception.Errors;

        Assert.Equal("Erro ao realizar o cadastro", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Contains("Documento já cadastrado", errors[0]);
    }
}