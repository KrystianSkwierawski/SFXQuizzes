using FluentValidation;

namespace Application.Quizzes.Commands.RateQuiz;

public class RateQuizCommandValidator : AbstractValidator<RateQuizCommand>
{
    public RateQuizCommandValidator()
    {
        RuleFor(quiz => quiz.RateValue)
            .GreaterThan(0)
            .LessThanOrEqualTo(5);
    }
}

