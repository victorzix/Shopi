using FluentValidation;
using Shopi.Product.Application.Commands;

namespace Shopi.Product.Application.Validators;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotNull().WithMessage("Nome da categoria não pode estar vazio");
    }
}