using DevFreela.Application.Commands.InsertSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertSkillValidator : AbstractValidator<InsertSkillCommand>
    {
        public InsertSkillValidator()
        {
            RuleFor(s => s.Description)
                .NotEmpty()
                    .WithMessage("A descrição é obrigatória")
                        .MaximumLength(100)
                            .WithMessage("A descrição deve conter até 100 caracteres");
        }
    }
}
