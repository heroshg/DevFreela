﻿using DevFreela.Application.Commands.InsertUser;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertUserValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                    .WithMessage("E-mail inválido");

            RuleFor(u => u.BirthDate)
                .Must(d => d < DateTime.Now.AddYears(-18))
                    .WithMessage("Deve ser maior de idade.");
        }
    }
}
