using CloudService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudService.Infrastructure.Configurations;

public class IpEntityTypeConfiguration : IEntityTypeConfiguration<Ip>
{

    public void Configure(EntityTypeBuilder<Ip> builder)
    {
        builder.ToTable("ip_configuration");
        builder.HasKey(i => i.IpId).HasName("pk_ip");
        builder.Property(i => i.IpId).ValueGeneratedNever();
        builder.Property(r=>r.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.HasData([
            new Ip { Name = "Dynamic IP", Price = 10.00M },
            new Ip { Name = "Static IP", Price = 20.00M }
        ]);
    }

}
