using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.Application.Commands;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Application.Interfaces;

namespace Shopi.Identity.API.CommandHandlers;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ApiResponses<LoginUserResponseDto>>
{
    private readonly IIdentityJwtService _jwtService;

    public ConfirmEmailCommandHandler(IIdentityJwtService jwtService)
    {
        _jwtService = jwtService;
    }


    public async Task<ApiResponses<LoginUserResponseDto>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        return await _jwtService.ConfirmEmail(request.Token);
    }
}