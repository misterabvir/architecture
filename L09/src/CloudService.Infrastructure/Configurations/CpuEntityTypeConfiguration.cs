using CloudService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudService.Infrastructure.Configurations;

public class CpuEntityTypeConfiguration : IEntityTypeConfiguration<Cpu>
{
    public void Configure(EntityTypeBuilder<Cpu> builder)
    {
        builder.ToTable("cpu_configuration");
        builder.HasKey(c=>c.CpuId).HasName("pk_cpu");
        builder.Property(c=>c.CpuId).HasColumnName("cpu_id").ValueGeneratedNever();
        builder.Property(r=>r.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.HasData([
            new Cpu { Name = "2 vCPUs", Price = 20.00M },
            new Cpu { Name = "4 vCPUs", Price = 40.00M },
            new Cpu { Name = "8 vCPUs", Price = 80.00M },
            new Cpu { Name = "16 vCPUs", Price = 160.00M }
        ]);
    }
}
