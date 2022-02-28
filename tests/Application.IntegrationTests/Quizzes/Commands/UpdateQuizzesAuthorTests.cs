using Application.Common.Exceptions;
using Application.Quizzes.Commands.UpdateQuizzesAuthor;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Commands;

using static Testing;
public class UpdateQuizzesAuthorTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new UpdateQuizzesAuthorCommand();

        await FluentActions.Invoking(() =>
             SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateQuizzesAuthor()
    {
        //Arrang
        await RunAsDefaultUserAsync();

        Quiz entity = await AddAsync(new Quiz
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = "test@local",
            Rates = new List<Rate>()
        });

        string userName = "updatedUserName@local";
        var command = new UpdateQuizzesAuthorCommand { UserName = userName };


        //Act
        await SendAsync(command);


        //Assert
        Quiz result = await FindAsync<Quiz>(entity.Id);
        result.Author.Should().Be(userName);
    }
}

