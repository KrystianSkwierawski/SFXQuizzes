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

        // https://www.audiomountain.com/tech/audio-file-size.html
        // 8Kbps Bitrate per seconds = 1KB. 300 seconds
        // 320Kbps Bitrate per second = 40KB. 7.5 seconds
        RuleForEach(vm => vm.CreateQuizVm.Files)
            .Must(file => file.Length < 300000); // around 300KB for each file

        RuleFor(vm => vm.CreateQuizVm.Files.Count())
            .LessThanOrEqualTo(30);
    }
}

