using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RobotCloudService.Authentications.Application.Users;
using RobotCloudService.Authentications.Application.Users.ValueObjects;

namespace RobotCloudService.Authentications.Infrastructure.Persistence.Configurations;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.UserId).HasName("pk_users");

        builder.Property(u => u.UserId)
            .IsRequired()
            .HasConversion(userId => userId.Value.ToString(), value => UserId.Create(Ulid.Parse(value)))
            .HasMaxLength(64)
            .HasColumnName("user_id");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasConversion(email => email.Value, value => Email.Create(value))
            .HasMaxLength(Email.MaxLength)
            .HasColumnName("email");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasConversion(password => password.Value, value => Password.Create(value))
            .HasMaxLength(Password.MaxLength)
            .HasColumnName("password");

        builder.Property(u => u.EmailVerified)
            .IsRequired()
            .HasColumnName("email_verified");

        builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(u => u.UpdateAt)
            .IsRequired()
            .HasColumnName("updated_at");
    }
}
