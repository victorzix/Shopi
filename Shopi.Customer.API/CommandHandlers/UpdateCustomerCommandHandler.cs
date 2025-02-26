using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Shopi.Core.Exceptions;
using Shopi.Core.Services;
using Shopi.Core.Utils;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;
using Shopi.Customer.API.Repository;
using Shopi.Customer.API.Validators;

namespace Shopi.Customer.API.CommandHandlers;

public class
    UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponses<CreateCustomerResponseDto>>
{
    private readonly ICustomerReadRepository _readRepository;
    private readonly ICustomerWriteRepository _writeRepository;
    private readonly IMapper _mapper;
    private readonly BffHttpClient _httpClient;

    public UpdateCustomerCommandHandler(ICustomerWriteRepository writeRepository, IMapper mapper,
        BffHttpClient httpClient, ICustomerReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
        _httpClient = httpClient;
        _readRepository = readRepository;
    }

    public async Task<ApiResponses<CreateCustomerResponseDto>> Handle(UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateCustomerValidator();
        var validate = validator.Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validate.Errors.Select(e => e.ErrorMessage));
        }

        var customerToUpdate = await _readRepository.FilterClient(new FilterCustomerQuery(null, null, request.Id));
        if (customerToUpdate == null)
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

        var mappedCustomer = _mapper.Map<UpdateCustomerCommand, AppCustomer>(request, customerToUpdate);
        var updatedCustomer = await _writeRepository.Update(mappedCustomer);

        return new ApiResponses<CreateCustomerResponseDto>
        {
            Data = _mapper.Map<CreateCustomerResponseDto>(updatedCustomer),
            Success = true
        };
    }
}