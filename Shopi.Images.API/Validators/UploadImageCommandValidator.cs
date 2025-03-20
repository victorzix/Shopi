using FluentValidation;
using Shopi.Images.API.Commands;
using Shopi.Images.API.Queries;

namespace Shopi.Images.API.Validators;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(u => u.ProductId).NotNull().WithMessage("Id do produto não pode estar vazio");
        RuleFor(u => u.FileName).NotNull().WithMessage("Nome do arquivo não pode estar vazio");
    }
}