using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.Application.Commands;
using Shopi.Product.Application.Validators;
using Shopi.Product.Domain.Entities;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponses<AppProduct>>
{
    private readonly IProductReadRepository _readRepository;
    private readonly IProductWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductReadRepository readRepository, IProductWriteRepository writeRepository,
        IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<AppProduct>> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _readRepository.Get(request.Id);
        if (product == null)
        {
            throw new CustomApiException("Erro ao realizar operação", StatusCodes.Status404NotFound,
                "Produto não encontrado");
        }

        var validator = new UpdateProductCommandValidator();
        var isValid = await validator.ValidateAsync(request, cancellationToken);
        if (!isValid.IsValid)
        {
            throw new CustomApiException("Erros de validação", StatusCodes.Status400BadRequest, isValid.Errors);
        }
        
        var updatedProduct = await _writeRepository.Update(_mapper.Map(request, product));

        return new ApiResponses<AppProduct>
        {
            Data = updatedProduct,
            Success = true
        };
    }
}