using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Quizzes.Queries.GetQuizzes;

public class QuizDto : IMapFrom<Quiz>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int NumberOfSFXs { get; set; }

    public DateTime Created { get; set; }

    public string Author { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quiz, QuizDto>()
            .ForMember(quizDto => quizDto.NumberOfSFXs, opt => opt.MapFrom(quiz => quiz.SFXs.Count()));
    }
}

