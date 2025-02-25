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

        var customer = await _customerRepository.FilterClient(new FilterCustomerQuery() { Id = request.CustomerId });

        var addressToUpdate = await _readRepository.Get(new GetAddressQuery(request.Id, request.CustomerId));

        if (addressToUpdate == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Endereço não encontrado");
        }

        var mappedAddress = _mapper.Map<UpdateAddressCommand, Address>(request, addressToUpdate);
        var updatedAddress = await _writeRepository.Update(mappedAddress);
        request.CustomerId = customer.Id;

        return new ApiResponses<CreateAddressResponse>
        {
            Data = _mapper.Map<CreateAddressResponse>(updatedAddress),
            Success = true
        };
    }
}