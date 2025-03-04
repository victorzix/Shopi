using FluentValidation;
using Shopi.Admin.API.Commands;

namespace Shopi.Admin.API.Validators;

public class UpdateAdminValidator : AbstractValidator<UpdateAdminCommand>
{
    public UpdateAdminValidator()
    {
        RuleFor(c => c.Email).EmailAddress()
            .When(c => !string.IsNullOrEmpty(c.Email))
            .WithMessage("Email inválido");
        RuleFor(c => c.Name).MinimumLength(3)
            .When(c => !string.IsNullOrEmpty(c.Name))
            .WithMessage("Nome deve possuir no minímo 3 caracteres");
    }
}