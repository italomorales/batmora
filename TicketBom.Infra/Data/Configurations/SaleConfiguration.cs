using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
        }
    }
}
