using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(p => p.TotalCost)
                .GreaterThanOrEqualTo(1000)
                    .WithMessage("O custo total do projeto deve ser no mínimo R$ 1.000");

            RuleFor(p => p.Title)
                .MaximumLength(50)
                    .WithMessage("Título deve conter no máximo 50 caracteres");
        }
    }
}
