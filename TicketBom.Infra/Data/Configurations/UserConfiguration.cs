using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketBom.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.AccountAggregate.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.AccountAggregate.User> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(36);
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Email).HasMaxLength(250).IsRequired();
            builder.Property(s => s.Mobile).HasMaxLength(15);
            builder.Property(s => s.Md5Pass).HasMaxLength(32);
        }
    }
}

