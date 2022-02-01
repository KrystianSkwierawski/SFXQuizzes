using Application.Quizzes.Commands.UpsertQuiz;
using Application.Quizzes.Queries.GetQuiz;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
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
        IList<IFormFile> files = new List<IFormFile>() {
            new FormFile(null, 0, 0, null, "sfx.wav")
        };

        UpsertQuizVm upsertQuizVm = new()
        {
            Title = "test",
            Files = files
        };

        var quizId = await SendAsync(new UpsertQuizCommand { UpsertQuizVm = upsertQuizVm });    

        GetQuizQuery query = new() { Id = quizId };

        //Act
        QuizDto result = await SendAsync(query);


        //Assert

        result.Should().NotBeNull();
        result.Title.Should().Be(upsertQuizVm.Title);

        // Application.IntegrationTests\bin\Debug\net6.0\wwwroot\assets\SFXs\{id}
        result.SFXs[0].Name.Should().Be(upsertQuizVm.Files[0].FileName);
    }
}

