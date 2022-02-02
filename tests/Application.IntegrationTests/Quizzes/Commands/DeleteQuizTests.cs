using Application.Common.Exceptions;
using Application.Quizzes.Commands.DeleteQuiz;
using Application.Quizzes.Commands.UpsertQuiz;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Commands;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Testing;
public class DeleteQuizTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidQuizId()
    {
        var command = new DeleteQuizCommand { Id = "999" };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }


    [Test]
    public async Task ShouldDeleteTask()
    {
        //Arrange
        await RunAsDefaultUserAsync();

        IList<IFormFile> files = new List<IFormFile>() {
            new FormFile(null, 0, 0, null, "sfx.wav")
        };

        var quizId = await SendAsync(new UpsertQuizCommand
        {
            UpsertQuizVm = new()
            {
                Title = "test",
                Files = files
            }
        });

        //Act
        await SendAsync(new DeleteQuizCommand { Id = quizId });

        //Assert
        var result = await FindAsync<Quiz>(quizId);
        result.Should().BeNull();

        // Application.IntegrationTests\bin\Debug\net6.0\wwwroot\assets\SFXs\{id}
        string directory = Path.Combine("./wwwroot", "assets", "SFXs", quizId);
        Directory.Exists(directory).Should().BeFalse();
    }

    [Test]
    public async Task ShouldThrowForbiddenAccessException_WhenDeletingQuizNotAsOwnerOrAdmin()
    {
        await RunAsAdministratorAsync();

        IList<IFormFile> files = new List<IFormFile>() {
            new FormFile(null, 0, 0, null, "sfx.wav")
        };

        var quizId = await SendAsync(new UpsertQuizCommand
        {
            UpsertQuizVm = new()
            {
                Title = "test",
                Files = files
            }
        });


        await RunAsDefaultUserAsync();

        DeleteQuizCommand command = new() { Id = quizId };

        await FluentActions.Invoking(() =>
                        SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();
    }
}

