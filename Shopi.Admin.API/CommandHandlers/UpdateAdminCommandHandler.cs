using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Shopi.Admin.Application.Commands;
using Shopi.Admin.Application.DTOs;
using Shopi.Admin.Application.Validators;
using Shopi.Admin.Domain.Entities;
using Shopi.Admin.Domain.Interfaces;
using Shopi.Admin.Domain.Queries;
using Shopi.Core.Exceptions;
using Shopi.Core.Services;
using Shopi.Core.Utils;

namespace Shopi.Admin.API.CommandHandlers;

public class
    UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, ApiResponses<CreateAdminResponseDto>>
{
    private readonly IAdminReadRepository _readRepository;
    private readonly IAdminWriteRepository _writeRepository;
    private readonly IMapper _mapper;
    private readonly BffHttpClient _httpClient;

    public UpdateAdminCommandHandler(IAdminWriteRepository writeRepository, IMapper mapper,
        BffHttpClient httpClient, IAdminReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
        _httpClient = httpClient;
        _readRepository = readRepository;
    }

    public async Task<ApiResponses<CreateAdminResponseDto>> Handle(UpdateAdminCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateAdminValidator();
        var validate = validator.Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validate.Errors.Select(e => e.ErrorMessage));
        }

        var adminToUpdate = await _readRepository.FilterAdmin(new QueryAdmin(null, request.Id));
        if (adminToUpdate == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Usuário não encontrado");
        }

        var userResponse = await _httpClient.PatchJsonAsync(MicroServicesUrls.AuthApiUrl, "update",
            _mapper.Map<UpdateUserDto>(request));

        if (!userResponse.IsSuccessStatusCode)
        {
            var errorContent = await userResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            throw new CustomApiException(deserializedErrorContent.Title, deserializedErrorContent.Status,
                deserializedErrorContent.Errors);
        }

        var mappedAdmin = _mapper.Map<UpdateAdminCommand, AppAdmin>(request, adminToUpdate);
        var updatedAdmin = await _writeRepository.Update(mappedAdmin);

        return new ApiResponses<CreateAdminResponseDto>
        {
            Data = _mapper.Map<CreateAdminResponseDto>(updatedAdmin),
            Success = true
        };
    }
}