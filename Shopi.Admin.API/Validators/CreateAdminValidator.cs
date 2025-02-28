using FluentValidation;
using Shopi.Admin.API.Commands;

namespace Shopi.Admin.API.Validators;

public class CreateAdminValidator : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminValidator()
    {
        RuleFor(c => c.Email).EmailAddress().WithMessage("Email inválido").NotEmpty()
            .WithMessage("Email não pode estar vazio");
        RuleFor(c => c.Name).NotEmpty().WithMessage("Nome não pode estar vazio").MinimumLength(3)
            .WithMessage("Nome precisa de no mínimo 3 caracteres");
    }
}