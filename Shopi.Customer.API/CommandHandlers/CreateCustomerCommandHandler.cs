using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
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
    CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResponses<CreateCustomerResponseDto>>
{
    private readonly ICustomerReadRepository _readRepository;
    private readonly ICustomerWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerWriteRepository writeRepository, IMapper mapper,
        ICustomerReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<ApiResponses<CreateCustomerResponseDto>> Handle(CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerValidator();
        var validateCreateCustomer = validator.Validate(request);

        if (!validateCreateCustomer.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validateCreateCustomer.Errors.Select(e => e.ErrorMessage).ToList());
        }

        await CheckEmailAndDocument(request);

        var customer = await _writeRepository.Create(_mapper.Map<AppCustomer>(request));
        return new ApiResponses<CreateCustomerResponseDto>
            { Success = true, Data = _mapper.Map<CreateCustomerResponseDto>(customer) };
    }

    private async Task CheckEmailAndDocument(CreateCustomerCommand request)
    {
        var emailInUse = await _readRepository.GetByEmailOrDocument(new GetByEmailOrDocumentQuery(
            request.Email,
            null));

        if (emailInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest, "Email já em uso");
        }

        var documentInUse = await _readRepository.GetByEmailOrDocument(new GetByEmailOrDocumentQuery(
            null,
            request.Document));

        if (documentInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                "Documento já cadastrado");
        }
    }
}