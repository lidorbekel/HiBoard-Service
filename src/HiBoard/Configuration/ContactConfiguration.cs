using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiBoard.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        // note: column names should be snake_cased!

        builder.ToTable("contacts");

        builder
            .Property(_ => _.Id)
            .HasColumnName("id");

        builder.HasKey(_ => _.Id);

        builder
            .Property(_ => _.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(_ => _.Birthdate)
            .HasColumnName("birthdate")
            .HasColumnType("DATE");
    }
}
