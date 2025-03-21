using FluentValidation;
using Shopi.Customer.Application.Commands;

namespace Shopi.Customer.API.Validators;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(c => c.Email).EmailAddress()
            .When(c => !string.IsNullOrEmpty(c.Email))
            .WithMessage("Email inválido");
        RuleFor(c => c.Name).MinimumLength(3)
            .When(c => !string.IsNullOrEmpty(c.Name))
            .WithMessage("Nome deve possuir no minímo 3 caracteres");
    }
}