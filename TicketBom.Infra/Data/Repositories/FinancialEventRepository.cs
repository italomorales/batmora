using System;
using System.Linq;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Repositories
{
    public class FinancialEventRepository : Repository<FinancialEvent>, IFinancialEventRepository
    {
        public FinancialEventRepository(TicketBomContext ctx) : base(ctx)
        {
        }
    }
}
