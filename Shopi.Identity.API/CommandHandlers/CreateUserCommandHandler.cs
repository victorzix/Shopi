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

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponses<CreateCustomerResponseDto>>
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

    public async Task<ApiResponses<CreateCustomerResponseDto>> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userData = await _identityJwtService.Register(_mapper.Map<RegisterUser>(request));

        var customerDto = _mapper.Map<CreateCustomerDto>(request);
        customerDto.UserId = userData.Data.UserId;

        var customerResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.CustomerApiUrl, "create", customerDto);
        if (!customerResponse.IsSuccessStatusCode)
        {
            var errorContent = await customerResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            await _identityJwtService.DeleteUser(userData.Data.UserId);
            throw new CustomApiException(deserializedErrorContent.Title, deserializedErrorContent.Status,
                deserializedErrorContent.Errors);
        }

        var content = await customerResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<CreateCustomerResponseDto>(content);
        return new ApiResponses<CreateCustomerResponseDto> { Data = deserializedContent, Success = true };
    }
}