using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Product.Application.Commands.ProductsCommands;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.CommandHandlers.ProductsCommandHandlers;

public class ChangeProductVisibilityCommandHandler : IRequestHandler<ChangeProductVisibilityCommand>
{
    private readonly IProductReadRepository _readRepository;
    private readonly IProductWriteRepository _writeRepository;

    public ChangeProductVisibilityCommandHandler(IProductReadRepository readRepository,
        IProductWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(ChangeProductVisibilityCommand request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.Get(request.Id);
        if (product == null)
        {
            throw new CustomApiException("Erro ao atualizar produto", StatusCodes.Status400BadRequest,
                "Produto não encontrado");
        }

        await _writeRepository.ChangeVisibility(product);

        product.Visible = !product.Visible;
    }
}