using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBom.Domain.Entities.AccountAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
        }
    }
}

