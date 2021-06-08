using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class ItemSaleConfiguration : IEntityTypeConfiguration<ItemSale>
    {
        public void Configure(EntityTypeBuilder<ItemSale> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
        }
    }
}
