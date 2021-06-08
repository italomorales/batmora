using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class FinancialEventConfiguration : IEntityTypeConfiguration<FinancialEvent>
    {
        public void Configure(EntityTypeBuilder<FinancialEvent> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
            builder.Property(s => s.Amount).HasPrecision(18, 2);
        }
    }
}

