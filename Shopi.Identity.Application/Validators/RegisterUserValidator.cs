using FluentValidation;
using Shopi.Identity.Domain.Entities;

namespace Shopi.Identity.Application.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido");
        RuleFor(x => x.Password).MinimumLength(3).WithMessage("Senha deve possuir no mínimo 3 caracteres").NotEmpty()
            .WithMessage("Senha não pode estar vazia");
    }
}