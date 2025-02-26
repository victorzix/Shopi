using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;
using Shopi.Customer.API.Validators;

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
        
        var customer = await _customerRepository.FilterClient(new FilterCustomerQuery { Id = request.CustomerId });

        request.CustomerId = customer.Id;
        
        var address = await _repository.Create(_mapper.Map<Address>(request));
        
        return new ApiResponses<CreateAddressResponse>
            { Data = _mapper.Map<CreateAddressResponse>(address), Success = true };
    }
}