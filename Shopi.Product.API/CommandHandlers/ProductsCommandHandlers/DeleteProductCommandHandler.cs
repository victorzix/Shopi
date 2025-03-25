using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Product.Application.Commands.ProductsCommands;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.CommandHandlers.ProductsCommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductReadRepository _readRepository;
    private readonly IProductWriteRepository _writeRepository;

    public DeleteProductCommandHandler(IProductReadRepository readRepository, IProductWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.Get(request.Id);
        if (product == null)
        {
            throw new CustomApiException("Erro ao realizar operação", StatusCodes.Status404NotFound,
                "Produto não encontrado");
        }

        await _writeRepository.Deactivate(product);
    }
}