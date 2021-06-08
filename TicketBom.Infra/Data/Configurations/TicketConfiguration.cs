using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
        }
    }
}
