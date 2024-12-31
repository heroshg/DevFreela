using DevFreela.Application.Commands.InsertComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertCommentValidator : AbstractValidator<InsertCommentCommand>
    {
        public InsertCommentValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                    .WithMessage("O conteúdo do comentário não pode ser vazio.")
                        .MaximumLength(200)
                            .WithMessage("O conteúdo do comentário não deve exceder 200 caracteres.");
        }
    }
}
