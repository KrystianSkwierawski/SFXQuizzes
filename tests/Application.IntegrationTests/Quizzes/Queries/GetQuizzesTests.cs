using Application.Quizzes.Commands.UpsertQuiz;
using Application.Quizzes.Queries.GetQuizzes;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
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
        var (userId, userName) = await RunAsDefaultUserAsync();

        Quiz entity = await AddAsync(new Quiz
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = userName,
            SFXs = new List<SFX>(),
            Rates = new List<Rate>() {
                new Rate { RatedBy = userId, Value = 5 },
                new Rate { RatedBy = userId, Value = 2 }
            }
        });

        GetQuizzesQuery query = new();

        //Act
        IList<QuizDto> result = await SendAsync(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        result[0].AverageRate.Should().Be(
                entity.Rates.Sum(rate => rate.Value) / entity.Rates.Count()
            );

        result[0].NumberOfSFXs.Should()
            .Be(entity.SFXs.Count());
    }

    [Test]
    public async Task ShouldReturnPublicAndApprovedQuizzes()
    {
        //Arrange
        var (adminserId, userName) = await RunAsAdministratorAsync();

        Quiz entity = await AddAsync(new Quiz
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = userName,
            Approved = true,
            IsPublic = true,
        });

        var user = await RunAsDefaultUserAsync();

        GetQuizzesQuery query = new() { QuizFilter = QuizFilter.PublicAndApproved };

        //Act
        IList<QuizDto> result = await SendAsync(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].Approved.Should().BeTrue();
        result[0].IsPublic.Should().BeTrue();
    }

    [Test]
    public async Task ShouldReturnCurrentUserQuizzes()
    {
        //Arrange
        var (userId, userName) = await RunAsDefaultUserAsync();

        Quiz entity = await AddAsync(new Quiz
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = userName,
            CreatedBy = userId,
        });

        GetQuizzesQuery query = new() { QuizFilter = QuizFilter.CurrentUser };

        //Act
        IList<QuizDto> result = await SendAsync(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].Author.Should().Be(userName);
    }
}

