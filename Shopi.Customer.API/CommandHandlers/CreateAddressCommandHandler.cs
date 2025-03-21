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

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResponses<CreateAddressResponse>>
{
    private readonly IAddressWriteRepository _repository;
    private readonly ICustomerReadRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(IAddressWriteRepository repository, ICustomerReadRepository customerRepository,
        IMapper mapper)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CreateAddressResponse>> Handle(CreateAddressCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateAddressValidator();
        var validate = validator.Validate(request);

        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validate.Errors.Select(e => e.ErrorMessage));
        }

        var query = _mapper.Map<QueryCustomer>(new FilterCustomerQuery() { Id = request.CustomerId });
        
        var customer = await _customerRepository.FilterClient(query);

        request.CustomerId = customer.Id;
        
        var address = await _repository.Create(_mapper.Map<Address>(request));
        
        return new ApiResponses<CreateAddressResponse>
            { Data = _mapper.Map<CreateAddressResponse>(address), Success = true };
    }
}