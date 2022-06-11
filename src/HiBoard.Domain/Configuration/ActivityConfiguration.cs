using HiBoard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiBoard.Domain.Configuration;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        #region Table Configuration

        builder.ToTable("activities");

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder
            .Property(_ => _.Id)
            .HasColumnName("id");

        builder.HasKey(_ => _.Id);

        builder
            .Property(_ => _.Title)
            .HasColumnName("title")
            .HasMaxLength(50);

        builder
            .Property(_ => _.Description)
            .HasColumnName("description")
            .HasMaxLength(4000)
            .IsRequired();

        builder
            .Property(_ => _.Tag)
            .HasColumnName("tag")
            .HasMaxLength(50);

        builder.Property(_ => _.Week)
            .HasColumnName("week")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(_ => _.TimeEstimation)
            .HasColumnName("time_estimation_in_ticks")
            .HasColumnType("long")
            .IsRequired();

        builder
            .Property(_ => _.CreatedAt)
            .HasColumnName("creation_at")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder
            .Property(_ => _.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .HasColumnType("tinyint");

        #endregion

        #region Relationship Configuration

        builder.HasMany(activity => activity.Templates)
            .WithMany(template => template.Activities)
            .UsingEntity(t => t.ToTable("templates"));

        #endregion

        #region Convertion Configuration

        builder.Property(activity => activity.TimeEstimation).HasConversion(
            time => time.Ticks,
            time => TimeSpan.FromTicks(time));

        #endregion
    }
}