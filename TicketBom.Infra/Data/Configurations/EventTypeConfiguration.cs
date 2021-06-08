using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
            builder.Property(s => s.Description).HasMaxLength(100).IsRequired();
            builder.Property(s => s.DebitCredit).HasConversion(new EnumToNumberConverter<EnumDebitCredit, int>());
        }
    }
}

