using FluentValidation;
using Shopi.Customer.Application.Commands;

namespace Shopi.Customer.API.Validators;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(c => c.Email).EmailAddress().WithMessage("Email inválido").NotEmpty()
            .WithMessage("Email não pode estar vazio");
        RuleFor(c => c.Document).NotEmpty().WithMessage("Documento não pode estar vazio");
        RuleFor(c => c.Name).NotEmpty().WithMessage("Nome não pode estar vazio").MinimumLength(3).WithMessage("Nome precisa de no mínimo 3 caracteres");
    }
}