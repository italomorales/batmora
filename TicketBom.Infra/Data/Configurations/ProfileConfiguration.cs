using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBom.Domain;
using TicketBom.Domain.Entities.AccountAggregate;

namespace TicketBom.Infra.Data.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
            builder.Property(s => s.Description).HasMaxLength(100).IsRequired();
        }
    }
}

