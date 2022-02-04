﻿using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Quizzes.Queries.GetQuiz;

public class QuizDto : IMapFrom<Quiz>
{
    public QuizDto()
    {
        SFXs = new List<SFX>();
    }

    public string Id { get; set; }

    public string Title { get; set; }

    public bool IsPublic { get; set; }

    public bool Approved { get; set; }

    public string Author { get; set; }

    public IList<SFX> SFXs { get; set; }
}

