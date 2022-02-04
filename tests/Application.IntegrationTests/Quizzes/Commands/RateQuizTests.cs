using Application.Common.Exceptions;
using Application.Quizzes.Commands.RateQuiz;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Commands;

using static Testing;
public class RateQuizTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new RateQuizCommand();

        await FluentActions.Invoking(() =>
             SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRateQuiz()
    {
        //Arrange
        var (userId, userName) = await RunAsDefaultUserAsync();

        Quiz entity = await AddAsync(new Quiz
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = userName,
            Rates = new List<Rate>()
        });

        float rate = 4.5f;

        var command = new RateQuizCommand
        {
            Id = entity.Id,
            RateValue = rate
        };


        //Act
        await SendAsync(command);


        //Assert

        var result = await FindAsync<Quiz>(entity.Id);

        result.Should().NotBeNull();
        result.Rates.Should().HaveCount(1);
        result.Rates.First().Value.Should().Be(rate);
    }

    [Test]
    public async Task ShouldReplacePreviousUserRateQuiz()
    {
        //Arrange
        var (userId, userName) = await RunAsDefaultUserAsync();

        Quiz entity = await AddAsync(new Quiz
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = userName,
            Rates = new List<Rate>()
        });

        float rate = 4.5f;

        var command = new RateQuizCommand
        {
            Id = entity.Id,
            RateValue = rate
        };


        //Act
        await SendAsync(command);
        await SendAsync(command);


        //Assert

        var result = await FindAsync<Quiz>(entity.Id);

        result.Should().NotBeNull();
        result.Rates.Should().HaveCount(1);
        result.Rates.First().Value.Should().Be(rate);
    }
}

