using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Admin.API.Commands;
using Shopi.Admin.API.DTOs;
using Shopi.Admin.API.Interfaces;
using Shopi.Admin.API.Models;
using Shopi.Admin.API.Queries;
using Shopi.Admin.API.Validators;

namespace Shopi.Admin.API.CommandHandlers;

public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ApiResponses<CreateAdminResponseDto>>
{
    private readonly IAdminReadRepository _readRepository;
    private readonly IAdminWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CreateAdminCommandHandler(IAdminWriteRepository writeRepository, IMapper mapper,
        IAdminReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<ApiResponses<CreateAdminResponseDto>> Handle(CreateAdminCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateAdminValidator();
        var validateCreateAdmin = validator.Validate(request);

        if (!validateCreateAdmin.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validateCreateAdmin.Errors.Select(e => e.ErrorMessage).ToList());
        }

        await CheckEmailAndDocument(request);

        var admin = await _writeRepository.Create(_mapper.Map<AppAdmin>(request));
        return new ApiResponses<CreateAdminResponseDto>
            { Success = true, Data = _mapper.Map<CreateAdminResponseDto>(admin) };
    }

    private async Task CheckEmailAndDocument(CreateAdminCommand request)
    {
        var emailInUse = await _readRepository.FilterAdmin(new FilterAdminQuery(
            request.Email,
            null));

        if (emailInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest, "Email já em uso");
        }

        var documentInUse = await _readRepository.FilterAdmin(new FilterAdminQuery(
            null,
            null));

        if (documentInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                "Documento já cadastrado");
        }
    }
}