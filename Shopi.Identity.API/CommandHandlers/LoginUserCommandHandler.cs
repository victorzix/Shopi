using AutoMapper;
using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.API.Services;
using Shopi.Identity.Application.Commands;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Domain.Entities;

namespace Shopi.Identity.API.CommandHandlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponses<LoginUserResponseDto>>
{
    private readonly IdentityJwtService _identityJwtService;
    private readonly IMapper _mapper;
    
    public LoginUserCommandHandler(IdentityJwtService identityJwtService, IMapper mapper)
    {
        _identityJwtService = identityJwtService;
        _mapper = mapper;
    }

    public async Task<ApiResponses<LoginUserResponseDto>> Handle(LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var userData = await _identityJwtService.Login(_mapper.Map<LoginUser>(request));
        return new ApiResponses<LoginUserResponseDto> { Data = userData.Data, Success = true};
    }
}