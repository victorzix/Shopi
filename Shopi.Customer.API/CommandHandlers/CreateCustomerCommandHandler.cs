using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.API.Validators;
using Shopi.Customer.Application.Commands;
using Shopi.Customer.Application.DTOs;
using Shopi.Customer.Application.Queries;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Interfaces;
using Shopi.Customer.Domain.Queries;

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
        var validateCreateCustomer = await validator.ValidateAsync(request, cancellationToken);

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
        var emailInUseQuery = _mapper.Map<QueryCustomer>(new FilterCustomerQuery(
            request.Email,
            null, null));

        var emailInUse = await _readRepository.FilterClient(emailInUseQuery);

        if (emailInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest, "Email já em uso");
        }

        var documentInUseQuery = _mapper.Map<QueryCustomer>(new FilterCustomerQuery(
            null,
            request.Document,
            null));

        var documentInUse = await _readRepository.FilterClient(documentInUseQuery);

        if (documentInUse != null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                "Documento já cadastrado");
        }
    }
}