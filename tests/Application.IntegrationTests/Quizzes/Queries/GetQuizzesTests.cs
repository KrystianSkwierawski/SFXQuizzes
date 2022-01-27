using Application.Quizzes.Queries.GetQuizzes;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizzes.Queries;

using static Testing;
public class GetQuizzesTests : TestBase
{
    [Test]
    public async Task ShouldReturnQuizzes()
    {
        //Arrange
        await AddRangeAsync(new List<Quiz>
        {
            new Quiz
            {
                Id = Guid.NewGuid().ToString(),
                Title = "quiz"
            },
            new Quiz
            {
                Id = Guid.NewGuid().ToString(),              
                Title = "quiz"
            }
        });

        GetQuizzesQuery query = new();

        //Act
        IList<QuizDto> result = await SendAsync(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }
}

