using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(TicketBomContext ctx) : base(ctx)
        {
        }
    }
}
