using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shopi.Core;
using Shopi.Core.Utils;
using Shopi.Identity.API.Commands;
using Shopi.Identity.API.DTOs;
using Shopi.Identity.API.Models;
using Shopi.Identity.API.Services;

namespace Shopi.Identity.API.CommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponses<CreateCustomerDto>>
{
    private readonly IdentityJwtService _identityJwtService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IdentityJwtService identityJwtService, IMapper mapper)
    {
        _identityJwtService = identityJwtService;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CreateCustomerDto>> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userData = await _identityJwtService.Register(_mapper.Map<RegisterUser>(request));
        
        var customerDto = _mapper.Map<CreateCustomerDto>(request);
        customerDto.UserId = userData.Data.UserId;
        return new ApiResponses<CreateCustomerDto> { Data = customerDto, Success = true};
    }
}