using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.Application.Commands;
using Shopi.Identity.Application.DTOs;
using Shopi.Identity.Application.Interfaces;

namespace Shopi.Identity.API.CommandHandlers;

public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendEmailConfirmationCommand>
{
    private readonly IIdentityJwtService _jwtService;

    public ResendConfirmationEmailCommandHandler(IIdentityJwtService jwtService)
    {
        _jwtService = jwtService;
    }


    public async Task Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        await _jwtService.ResendConfirmationEmail(request.Token);
    }
}