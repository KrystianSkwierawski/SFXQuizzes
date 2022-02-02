using Application.Quizzes.Commands.UpsertQuiz;
using Application.Quizzes.Queries.GetQuizzes;
using Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Queries;

using static Testing;
public class GetQuizzesTests : TestBase
{
    [Test]
    public async Task ShouldReturnQuizzes()
    {
        //Arrange
        await RunAsDefaultUserAsync();

        IList<IFormFile> files = new List<IFormFile>() {
            new FormFile(null, 0, 0, null, "sf1.wav"),
            new FormFile(null, 0, 0, null, "sfx2.wav")
        };

        await SendAsync(new UpsertQuizCommand()
        {
            UpsertQuizVm = new UpsertQuizVm()
            {
                Title = "quiz",
                Files = files
            }
        });

        GetQuizzesQuery query = new();

        //Act
        IList<QuizDto> result = await SendAsync(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        result[0].NumberOfSFXs.Should()
            .Be(files.Count());
    }

    [Test]
    public async Task ShouldReturnPublicAndApprovedQuizzes()
    {
        //Arrange
        await RunAsAdministratorAsync();

        IList<IFormFile> files = new List<IFormFile>() {
            new FormFile(null, 0, 0, null, "sf1.wav"),
            new FormFile(null, 0, 0, null, "sfx2.wav")
        };

        await SendAsync(new UpsertQuizCommand()
        {
            UpsertQuizVm = new UpsertQuizVm()
            {
                Title = "quiz",
                Files = files,
                IsPublic = true,
                Approved = true
            }
        });

        var user = await RunAsDefaultUserAsync();

        GetQuizzesQuery query = new() { QuizFilter = QuizFilter.PublicAndApproved };

        //Act
        IList<QuizDto> result = await SendAsync(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].Author.Should().NotBe(user.Item2);

        result[0].NumberOfSFXs.Should()
            .Be(files.Count());
    }

    [Test]
    public async Task ShouldReturnCurrentUserQuizzes()
    {
        //Arrange
        var (userId, userName) = await RunAsDefaultUserAsync();

        IList<IFormFile> files = new List<IFormFile>() {
            new FormFile(null, 0, 0, null, "sf1.wav"),
            new FormFile(null, 0, 0, null, "sfx2.wav")
        };

        await SendAsync(new UpsertQuizCommand()
        {
            UpsertQuizVm = new UpsertQuizVm()
            {
                Title = "quiz",
                Files = files
            }
        });

        GetQuizzesQuery query = new() { QuizFilter = QuizFilter.CurrentUser };

        //Act
        IList<QuizDto> result = await SendAsync(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].Author.Should().Be(userName);

        result[0].NumberOfSFXs.Should()
            .Be(files.Count());
    }
}

