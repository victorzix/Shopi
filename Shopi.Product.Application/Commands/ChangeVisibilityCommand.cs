using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs;

namespace Shopi.Product.Application.Commands;

public class ChangeVisibilityCommand : IRequest
{
    public Guid Id { get; set; }

    public ChangeVisibilityCommand(Guid id)
    {
        Id = id;
    }
}