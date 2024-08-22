using CloudService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudService.Infrastructure.Configurations;

public class OsEntityTypeConfiguration : IEntityTypeConfiguration<Os>
{

    public void Configure(EntityTypeBuilder<Os> builder)
    {
        builder.ToTable("os_configuration");
        builder.HasKey(o=>o.OsId).HasName("pk_os");
        builder.Property(o=>o.OsId).ValueGeneratedNever();
        builder.Property(r=>r.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.HasData([
            new Os { Name = "Linux Ubuntu", Price = 0.00M },
            new Os { Name = "Windows 10", Price = 30.00M },
            new Os { Name = "Windows Server", Price = 50.00M },
            new Os { Name = "Red Hat Enterprise Linux", Price = 25.00M }
        ]);
    }
}
