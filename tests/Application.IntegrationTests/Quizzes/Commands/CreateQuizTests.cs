using Application.Common.Exceptions;
using Application.Quizzes.Commands.CreateQuiz;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Commands;

using static Testing;
public class CreateQuizTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateQuizCommand();

        await FluentActions.Invoking(() =>
             SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }


    [Test]
    public async Task ShouldCreateQuiz()
    {
        //Arrang
        IList<IFormFile> files = new List<IFormFile>() {
            new FormFile(null, 0, 0, null, "sfx.wav")
        };


        CreateQuizCommand command = new()
        {
            CreateQuizVm = new()
            {
                Title = "test",
                Files = files
            }
        };

        //Act
        var quizId = await SendAsync(command);

        //Assert
        Quiz result = await FindAsync<Quiz>(quizId);

        result.Should().NotBeNull();
        result.Title.Should().Be(command.CreateQuizVm.Title);

        // Application.IntegrationTests\bin\Debug\net6.0\wwwroot\assets\SFXs\{id}
        result.SFXs[0].Name.Should().Be(files[0].FileName);
    }
}

