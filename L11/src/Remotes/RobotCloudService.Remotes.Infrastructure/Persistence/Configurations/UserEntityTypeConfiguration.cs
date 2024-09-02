using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Infrastructure.Persistence.Configurations;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.UserId).HasName("pk_users");

        builder.Property(u => u.UserId)
            .IsRequired()
            .HasConversion(userId => userId.Value.ToString(), value => UserId.Create(Ulid.Parse(value)))
            .HasColumnName("user_id");

        builder.OwnsMany(u => u.Rooms, RoomConfigure);
        builder.OwnsMany(u => u.Robots, RobotConfigure);
        builder.OwnsMany(r => r.Logs, LogConfigure);
        builder.Metadata.FindNavigation(nameof(User.Rooms))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(User.Robots))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(User.Logs))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void RoomConfigure(OwnedNavigationBuilder<User, Room> builder)
    {
        builder.ToTable("rooms");

        builder.WithOwner().HasForeignKey(r => r.UserId);

        builder.HasKey(u => new { u.UserId, u.RoomId }).HasName("pk_rooms");

        builder.Property(r => r.RoomId)
            .IsRequired()
            .HasConversion(roomId => roomId.Value.ToString(), value => RoomId.Create(Ulid.Parse(value)))
            .HasColumnName("room_id");

        builder.Property(r => r.UserId)
            .IsRequired()
            .HasConversion(userId => userId.Value.ToString(), value => UserId.Create(Ulid.Parse(value)))
            .HasColumnName("user_id");

        builder.Property(r => r.Title)
            .IsRequired()
            .HasConversion(title => title.Value, value => Title.Create(value))
            .HasMaxLength(100)
            .HasColumnName("title");

        builder.Property(r => r.Area)
            .IsRequired()
            .HasConversion(square => square.Value, value => Area.Create(value))
            .HasMaxLength(100)
            .HasColumnName("square");

        builder.Property(r => r.LastCleanedAt)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("last_cleaned_at");
    }

    private void RobotConfigure(OwnedNavigationBuilder<User, Robot> builder)
    {
        builder.ToTable("robots");

        builder.WithOwner().HasForeignKey(r => r.UserId);

        builder.HasKey(u => new { u.UserId, u.RobotId }).HasName("pk_robots");

        builder.Property(r => r.RobotId)
            .IsRequired()
            .HasConversion(robotId => robotId.Value.ToString(), value => RobotId.Create(Ulid.Parse(value)))
            .HasColumnName("robot_id");

        builder.Property(r => r.UserId)
            .IsRequired()
            .HasConversion(userId => userId.Value.ToString(), value => UserId.Create(Ulid.Parse(value)))
            .HasColumnName("user_id");

        builder.Property(r => r.RoomId)
            .IsRequired()
            .HasConversion(roomId=> roomId.Value.ToString(), value => RoomId.Create(Ulid.Parse(value)))
            .HasColumnName("room_id");

        builder.Property(r => r.RobotState)
            .IsRequired()
            .HasConversion(state => state.Value, value => State.Create(value))
            .HasMaxLength(100)
            .HasColumnName("state");

        builder.Property(r => r.Model)
            .IsRequired()
            .HasConversion(model => model.Value, value => Model.Create(value))
            .HasMaxLength(100)
            .HasColumnName("model");

        builder.Property(r => r.Speed)
            .IsRequired()
            .HasConversion(speed => speed.Value, value => Speed.Create(value))
            .HasMaxLength(100)
            .HasColumnName("speed");

        builder.Property(r => r.CalculatedTimeOfCleaningOver)
            .IsRequired()
            .HasColumnName("calculated_time_of_cleaning_over");
    }

    private void LogConfigure(OwnedNavigationBuilder<User, Log> builder)
    {
        builder.ToTable("logs");

        builder.WithOwner().HasForeignKey(l => l.UserId);

        builder.HasKey(l => new { l.UserId, l.LogId }).HasName("pk_logs");

        builder.Property(l => l.LogId)
            .IsRequired()
            .HasConversion(logId => logId.Value.ToString(), value => LogId.Create(Ulid.Parse(value)))
            .HasColumnName("log_id");

        builder.Property(l => l.UserId)
            .IsRequired()
            .HasConversion(userId => userId.Value.ToString(), value => UserId.Create(Ulid.Parse(value)))
            .HasColumnName("user_id");

        builder.Property(l => l.Message)
            .IsRequired()
            .HasColumnName("message");

        builder.Property(l => l.OccuredAt)
            .IsRequired()
            .HasColumnName("occured_at");
    }
}
