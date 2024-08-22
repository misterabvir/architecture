using CloudService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudService.Infrastructure.Configurations;

public class RomEntityTypeConfiguration : IEntityTypeConfiguration<Rom>
{

    public void Configure(EntityTypeBuilder<Rom> builder)
    {
        builder.ToTable("rom_configuration");
        builder.HasKey(r=>r.RomId).HasName("pk_rom");
        builder.Property(r=>r.RomId).HasColumnName("rom_id").ValueGeneratedNever();
        builder.Property(r=>r.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.HasData([
            new Rom { Name = "128 GB", Price = 50.00M },
            new Rom { Name = "256 GB", Price = 100.00M },
            new Rom { Name = "512 GB", Price = 200.00M },
            new Rom { Name = "1 TB", Price = 400.00M }
        ]);    
    }
}
