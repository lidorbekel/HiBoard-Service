using System.Runtime.CompilerServices;
using HiBoard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace HiBoard.Domain.Configuration;

public class UserActivitiesConfiguration : IEntityTypeConfiguration<UserActivity>
{
    public void Configure(EntityTypeBuilder<UserActivity> builder)
    {
        #region Table Configuration

        builder.ToTable("user_activities");

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder
            .Property(_ => _.Id)
            .HasColumnName("id");

        builder.HasKey(_ => _.Id);
        
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

        builder.Property(_ => _.ActivityId)
            .HasColumnName("activity_id");

        builder.Property(_ => _.UserId)
            .HasColumnName("user_id");

        builder.Property(_ => _.Status)
            .HasColumnName("status");

        #endregion

        #region Releationship Configuration

        builder
            .HasOne(userActivity => userActivity.Activity)
            .WithMany(activity => activity!.UserActivities)
            .HasForeignKey(userActivity => userActivity.ActivityId)
            .IsRequired();

        builder
            .HasOne(userActivity => userActivity.User)
            .WithMany(user => user!.UserActivities)
            .HasForeignKey(userActivity => userActivity.UserId)
            .IsRequired();

        #endregion

        #region Convertion Configuration



        #endregion
    }
}