using FluentValidation;

namespace Application.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(vm => vm.CreateQuizVm.Title)
            .MaximumLength(50)
            .NotNull()
            .NotEmpty();

        RuleFor(vm => vm.CreateQuizVm.Files)
            .NotNull()
            .NotEmpty();
    }
}

