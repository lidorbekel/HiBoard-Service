using HiBoard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiBoard.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // note: column names should be snake_cased!

        builder.ToTable("users");

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder
            .Property(_ => _.Id)
            .HasColumnName("id");

        builder.HasKey(_ => _.Id);

        builder
            .Property(_ => _.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(_ => _.Title)
            .HasColumnName("title")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(_ => _.Role)
            .HasColumnName("role")
            .IsRequired();

        builder
            .Property(_ => _.Department)
            .HasColumnName("department")
            .IsRequired();

        builder
            .Property(_ => _.CreationDate)
            .HasColumnName("creation_date")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .HasColumnType("tinyint(1)");
    }
}
