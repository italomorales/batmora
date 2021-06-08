using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class PointOfSaleConfiguration : IEntityTypeConfiguration<PointOfSale>
    {
        public void Configure(EntityTypeBuilder<PointOfSale> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
        }
    }
}