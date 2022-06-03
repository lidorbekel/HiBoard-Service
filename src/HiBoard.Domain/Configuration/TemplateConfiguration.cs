using HiBoard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiBoard.Domain.Configuration;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        #region Table Configuration

        builder.ToTable("templates");

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder
            .Property(_ => _.Id)
            .HasColumnName("id");

        builder.HasKey(_ => _.Id);

        builder
            .Property(_ => _.Name)
            .HasColumnName("name")
            .IsRequired();

        builder
            .Property(_ => _.CompanyId)
            .HasColumnName("company_id")
            .IsRequired();

        builder
            .Property(_ => _.Department)
            .HasColumnName("department")
            .IsRequired();

        builder
            .Property(_ => _.Department)
            .HasColumnName("department")
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

        #region Releationship Configuration

        builder.HasMany(template => template.Activities)
            .WithMany(Activity => Activity.Templates!)
            .UsingEntity(t => t.ToTable("template_activities"));

        #endregion
    }
}