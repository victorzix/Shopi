using FluentValidation;
using Shopi.Images.Application.Commands;
using Shopi.Images.Application.Queries;

namespace Shopi.Images.Application.Validators;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(u => u.ProductId).NotNull().WithMessage("Id do produto não pode estar vazio");
        RuleFor(u => u.FileName).NotNull().WithMessage("Nome do arquivo não pode estar vazio");
    }
}