using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Quiz), typeof(Application.Quizzes.Queries.GetQuiz.QuizDto))]
    [TestCase(typeof(Quiz), typeof(Application.Quizzes.Queries.GetQuizzes.QuizDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type);

        return FormatterServices.GetUninitializedObject(type);
    }
}

