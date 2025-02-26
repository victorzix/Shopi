using AutoMapper;
using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.API.Commands;
using Shopi.Identity.API.DTOs;
using Shopi.Identity.API.Services;

namespace Shopi.Identity.API.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponses<string>>
{
    private readonly IdentityJwtService _identityJwtService;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IdentityJwtService identityJwtService, IMapper mapper)
    {
        _identityJwtService = identityJwtService;
        _mapper = mapper;
    }

    public async Task<ApiResponses<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _identityJwtService.UpdateUser(_mapper.Map<UpdateUserDto>(request));
        return new ApiResponses<string> { Data = "Ok", Success = true };
    }
}