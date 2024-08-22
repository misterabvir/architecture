using CloudService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudService.Infrastructure.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.UserId).HasName("pk_users");
        builder.Property(u => u.UserId).HasColumnName("user_id").ValueGeneratedNever();
        builder.Property(u => u.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
        builder.Property(u => u.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
        builder.HasMany(u => u.Configs).WithOne().HasForeignKey(c => c.UserId).IsRequired();

        builder.HasIndex(u => u.Username, "idx_users_username").IsUnique();
        builder.HasIndex(u => u.UserId, "idx_users_id").IsUnique();
    }

}


public class SetupEntityTypeConfiguration : IEntityTypeConfiguration<Setup>
{
    public void Configure(EntityTypeBuilder<Setup> builder)
    {
        builder.ToTable("setups");
        builder.HasKey(o => o.SetupId).HasName("pk_setups");
        builder.Property(o => o.SetupId).HasColumnName("setup_id").ValueGeneratedNever();
        builder.Property(o => o.Status).HasConversion(status => status.Value, value => Status.Create(value)).HasMaxLength(31);
        builder.HasOne(o => o.Cpu).WithMany().HasForeignKey(o => o.CpuId).IsRequired();
        builder.HasOne(o => o.Ram).WithMany().HasForeignKey(o => o.RamId).IsRequired();
        builder.HasOne(o => o.Rom).WithMany().HasForeignKey(o => o.RomId).IsRequired();
        builder.HasOne(o => o.Ip).WithMany().HasForeignKey(o => o.IpId).IsRequired();
        builder.HasOne(o => o.Os).WithMany().HasForeignKey(o => o.OsId).IsRequired();
    }
}
