using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Quizzes.Queries.GetQuizzes;

public class QuizDto : IMapFrom<Quiz>
{
    public string Id { get; set; }

    public string Title { get; set; }

    public int NumberOfSFXs { get; set; }

    public DateTime Created { get; set; }

    public bool IsPublic { get; set; }

    public bool Approved { get; set; }

    public string Author { get; set; }

    public double AverageRate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quiz, QuizDto>()
            .ForMember(quizDto => quizDto.NumberOfSFXs, opt => opt.MapFrom(quiz => quiz.SFXs.Count()))
            .ForMember(quizDto => quizDto.AverageRate, opt => opt.MapFrom(quiz =>
                CountAverageRate(quiz.Rates)
            ));
    }

    private static double CountAverageRate(IList<Rate> rates)
    {
        double sum = rates.Sum(rate => rate.Value);

        double result = (sum > 0) ? sum / rates.Count() : 0;

        return result;
    }
}

