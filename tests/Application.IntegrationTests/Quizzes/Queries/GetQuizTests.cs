using Application.Quizzes.Queries.GetQuiz;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Queries;


using static Testing;
public class GetQuizTests : TestBase
{
    [Test]
    public async Task ShouldReturnQuiz()
    {
        //Arrange 

        var (userId, userName) = await RunAsDefaultUserAsync();

        Quiz entity = await AddAsync<Quiz>(new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = "user",
            Rates = new List<Rate>
            {
                new() { RatedBy = userId, Value = 5 }
            }
        });


        //Act
        QuizDto result = await SendAsync(new GetQuizQuery { Id = entity.Id });


        //Assert

        result.Should().NotBeNull();
        result.Title.Should().Be(result.Title);
        result.UserRate.Should().Be(entity.Rates[0].Value);  
    }
}

