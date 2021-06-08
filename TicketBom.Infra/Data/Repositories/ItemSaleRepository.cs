using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Repositories
{
    public class ItemSaleRepository : Repository<ItemSale>, IItemSaleRepository
    {
        public ItemSaleRepository(TicketBomContext ctx) : base(ctx)
        {
        }
    }
}
