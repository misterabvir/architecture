using CloudService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudService.Infrastructure.Configurations;

public class RamEntityTypeConfiguration : IEntityTypeConfiguration<Ram>
{
    public void Configure(EntityTypeBuilder<Ram> builder)
    {
        builder.ToTable("ram_configuration");
        builder.HasKey(r=>r.RamId).HasName("pk_ram");
        builder.Property(r=>r.RamId).HasColumnName("ram_id").ValueGeneratedNever();
        builder.Property(r=>r.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.HasData([
            new Ram { Name = "2 GB", Price = 30.00M },
            new Ram { Name = "4 GB", Price = 60.00M },
            new Ram { Name = "8 GB", Price = 120.00M },
            new Ram { Name = "16 GB", Price = 240.00M }
        ]);
    }
}
