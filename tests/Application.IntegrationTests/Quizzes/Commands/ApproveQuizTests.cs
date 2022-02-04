using Application.Quizzes.Commands.ApproveQuiz;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Commands;

using static Testing;
public class ApproveQuizTests : TestBase
{
    [Test]
    public async Task ShouldApproveQuiz()
    {
        //Arrange
        Quiz entity = await AddAsync<Quiz>(new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = "user",
            Approved = false
        });

        var command = new ApproveQuizCommand() { Id = entity.Id };

        //Act
        await SendAsync(command);

        //Assert
        Quiz result = await FindAsync<Quiz>(entity.Id);

        result.Should().NotBeNull();
        result.Approved.Should().BeTrue();
    }
}

