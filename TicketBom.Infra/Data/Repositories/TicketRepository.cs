using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketBomContext ctx) : base(ctx)
        {
        }
    }
}
