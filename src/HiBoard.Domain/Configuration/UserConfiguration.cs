using HiBoard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiBoard.Domain.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        #region Table Configuration

        builder.ToTable("users");

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder
            .Property(_ => _.Id)
            .HasColumnName("id");

        builder.HasKey(_ => _.Id);

        builder
            .Property(_ => _.Email)
            .HasColumnName("email")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(_ => _.ManagerId)
            .HasColumnName("manager_id");
        
        builder
            .Property(_ => _.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(50);

        builder
            .Property(_ => _.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(50);

        builder
            .Property(_ => _.Role)
            .HasColumnName("role")
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

        builder.Property(_ => _.CompanyId)
            .HasColumnName("company_id");


        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .HasColumnType("tinyint");

        #endregion

        #region Releationship Configuration

        //builder
        //    .HasOne(user => user.Company)
        //    .WithMany(company => company!.Users)
        //    .HasForeignKey(user => user.CompanyId)
        //    .IsRequired();

        builder.HasMany(user => user.UserActivities)
            .WithOne(userActivity => userActivity.User!)
            .HasForeignKey(x => x.UserId);

        #endregion
    }
}