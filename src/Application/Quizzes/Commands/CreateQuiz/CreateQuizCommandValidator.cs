using Application.Common.Validators;
using FluentValidation;

namespace Application.Quizzes.Commands.CreateQuiz;

using static FileFormatValidator;
using static CaptachaValidator;
public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(command => command.CreateQuizVm.Title)
            .MaximumLength(50)
            .NotNull()
            .NotEmpty();

        RuleFor(vm => vm.CreateQuizVm.Files)
            .NotNull()
            .NotEmpty();

        // https://www.audiomountain.com/tech/audio-file-size.html
        // 8Kbps Bitrate per seconds = 1KB. 300 seconds
        // 320Kbps Bitrate per second = 40KB. 7.5 seconds
        RuleForEach(command => command.CreateQuizVm.Files)
            .Must(file => file.Length < (300 * 1024)) // 300KB for each file
            .Must(file => HasSupportedFileFormat(file.FileName));

        RuleFor(command => command.CreateQuizVm.Files.Count())
            .LessThanOrEqualTo(30);

        RuleFor(command => command.CreateQuizVm.Captacha)
            .MustAsync(BeValid);
    }
}

