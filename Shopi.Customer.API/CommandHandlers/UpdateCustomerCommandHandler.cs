using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Shopi.Core.Exceptions;
using Shopi.Core.Services;
using Shopi.Core.Utils;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Repository;
using Shopi.Customer.API.Validators;

namespace Shopi.Customer.API.CommandHandlers;

public class
    UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponses<CreateCustomerResponseDto>>
{
    private readonly CustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly BffHttpClient _httpClient;

    public UpdateCustomerCommandHandler(CustomerRepository repository, IMapper mapper, BffHttpClient httpClient)
    {
        _repository = repository;
        _mapper = mapper;
        _httpClient = httpClient;
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

        var customerToUpdate = await _repository.GetById(request.Id);
        if (customerToUpdate == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Usuário não encontrado");
        }
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(_mapper.Map<UpdateUserDto>(request));
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
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
        var updatedCustomer = await _repository.Update(mappedCustomer);

        return new ApiResponses<CreateCustomerResponseDto>
        {
            Data = _mapper.Map<CreateCustomerResponseDto>(updatedCustomer),
            Success = true
        };
    }
}