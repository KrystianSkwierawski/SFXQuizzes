using Application.Quizzes.Commands.CreateQuiz;
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

        CreateQuizVm createQuizVm = new()
        {
            Title = "test",
            Files = files
        };

        var quizId = await SendAsync(new CreateQuizCommand { CreateQuizVm = createQuizVm});    

        GetQuizQuery query = new() { Id = quizId };

        //Act
        QuizDto result = await SendAsync(query);


        //Assert

        result.Should().NotBeNull();
        result.Title.Should().Be(createQuizVm.Title);

        // Application.IntegrationTests\bin\Debug\net6.0\wwwroot\assets\SFXs\{id}
        result.SFXs[0].Name.Should().Be(createQuizVm.Files[0].FileName);
    }
}

