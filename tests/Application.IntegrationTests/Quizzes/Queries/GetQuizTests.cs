using Application.Quizzes.Queries.GetQuiz;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Queries;


using static Testing;
public class GetQuizTests : TestBase
{
    [Test]
    public async Task ShouldReturnQuiz()
    {
        //Arrange 
        Quiz entity = await AddAsync<Quiz>(new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            Author = "user"
        });


        //Act
        QuizDto result = await SendAsync(new GetQuizQuery { Id = entity.Id });


        //Assert

        result.Should().NotBeNull();
        result.Title.Should().Be(result.Title);
    }
}

