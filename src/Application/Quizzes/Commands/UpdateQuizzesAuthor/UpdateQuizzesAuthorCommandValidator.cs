using FluentValidation;

namespace Application.Quizzes.Commands.UpdateQuizzesAuthor;

public class UpdateQuizzesAuthorCommandValidator : AbstractValidator<UpdateQuizzesAuthorCommand>
{
    public UpdateQuizzesAuthorCommandValidator()
    {
        RuleFor(quiz => quiz.UserName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(25);
    }
}

