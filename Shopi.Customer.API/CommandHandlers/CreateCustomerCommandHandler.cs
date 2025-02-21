using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;
using Shopi.Customer.API.Repository;
using Shopi.Customer.API.Validators;

namespace Shopi.Customer.API.CommandHandlers;

public class
    CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResponses<CreateCustomerResponseDto>>
{
    private readonly CustomerRepository _repository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(CustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ApiResponses<CreateCustomerResponseDto>> Handle(CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        await CheckEmailAndDocument(request);

        var validator = new CreateCustomerValidator();
        var validateCreateCustomer = validator.Validate(request);

        if (!validateCreateCustomer.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validateCreateCustomer.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var customer = await _repository.Create(_mapper.Map<AppCustomer>(request));
        return new ApiResponses<CreateCustomerResponseDto>
            { Success = true, Data = _mapper.Map<CreateCustomerResponseDto>(customer) };
    }

    private async Task CheckEmailAndDocument(CreateCustomerCommand request)
    {
        var emailInUse = await _repository.GetByEmailOrDocument(new GetByEmailOrDocumentQuery
        {
            Email = request.Email
        });

        if (emailInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest, "Email já em uso");
        }

        var documentInUse = await _repository.GetByEmailOrDocument(new GetByEmailOrDocumentQuery
        {
            Email = request.Document
        });

        if (documentInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                "Documento já cadastrado");
        }
    }
}