using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Shopi.Core;
using Shopi.Core.Exceptions;
using Shopi.Core.Interfaces;
using Shopi.Core.Services;
using Shopi.Core.Utils;
using Shopi.Identity.API.Commands;
using Shopi.Identity.API.DTOs;
using Shopi.Identity.API.Interfaces;
using Shopi.Identity.API.Models;
using Shopi.Identity.API.Services;

namespace Shopi.Identity.API.CommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponses<LoginUserResponseDto>>
{
    private readonly IBffHttpClient _httpClient;
    private readonly IIdentityJwtService _identityJwtService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IIdentityJwtService identityJwtService, IMapper mapper, IBffHttpClient httpClient)
    {
        _identityJwtService = identityJwtService;
        _mapper = mapper;
        _httpClient = httpClient;
    }

    public async Task<ApiResponses<LoginUserResponseDto>> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Role == "Customer")
        {
            if (request.Document == null)
            {
                throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                    "Documento é obrigatório");
            }
        }

        var userData = await _identityJwtService.Register(_mapper.Map<RegisterUser>(request));

        var customerDto = _mapper.Map<CreateCustomerDto>(request);
        customerDto.UserId = userData.Data.UserId;

        var url = request.Role == "Customer" ? MicroServicesUrls.CustomerApiUrl : MicroServicesUrls.AdminApiUrl;

        var customerResponse = await _httpClient.PostJsonAsync(url, "create", customerDto);
        if (!customerResponse.IsSuccessStatusCode)
        {
            var errorContent = await customerResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            await _identityJwtService.DeleteUser(userData.Data.UserId);
            throw new CustomApiException(deserializedErrorContent.Title, deserializedErrorContent.Status,
                deserializedErrorContent.Errors);
        }

        var loginData = await _identityJwtService.Login(new LoginUser
            { Email = request.Email, Password = request.Password });
        return new ApiResponses<LoginUserResponseDto> { Data = loginData.Data, Success = true };
    }
}