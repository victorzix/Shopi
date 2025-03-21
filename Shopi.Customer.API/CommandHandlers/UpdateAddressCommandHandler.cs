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

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ApiResponses<CreateAddressResponse>>
{
    private readonly IAddressWriteRepository _writeRepository;
    private readonly IAddressReadRepository _readRepository;
    private readonly ICustomerReadRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateAddressCommandHandler(IAddressWriteRepository writeRepository, IAddressReadRepository readRepository,
        ICustomerReadRepository customerRepository, IMapper mapper)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CreateAddressResponse>> Handle(UpdateAddressCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateAddressValidator();
        var validate = validator.Validate(request);

        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validate.Errors.Select(e => e.ErrorMessage));
        }

        var customerQuery = _mapper.Map<QueryCustomer>(new FilterCustomerQuery { Id = request.CustomerId });

        var customer = await _customerRepository.FilterClient(customerQuery);

        var addressQuery = _mapper.Map<QueryAddress>(new GetAddressQuery(request.Id, request.CustomerId));

        var addressToUpdate = await _readRepository.Get(addressQuery);

        if (addressToUpdate == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Endereço não encontrado");
        }
        
        var mappedAddress = _mapper.Map(request, addressToUpdate);
        var updatedAddress = await _writeRepository.Update(mappedAddress);
        request.CustomerId = customer.Id;

        return new ApiResponses<CreateAddressResponse>
        {
            Data = _mapper.Map<CreateAddressResponse>(updatedAddress),
            Success = true
        };
    }
}