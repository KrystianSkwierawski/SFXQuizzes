using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.Property(quiz => quiz.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .OwnsMany(quiz => quiz.SFXs);

        builder
            .OwnsMany(quiz => quiz.Rates);

        //builder.Property(quiz => quiz.Rates.Select(rate => rate.Value))
        //    .HasMaxLength(5);
    }
}

