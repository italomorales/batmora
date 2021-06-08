using System;
using System.Linq;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Infra.Data.Repositories
{
    public class PointOfSaleRepository : Repository<PointOfSale>, IPointOfSaleRepository
    {
        public PointOfSaleRepository(TicketBomContext ctx) : base(ctx)
        {
        }

        public bool OpenPOSExistsSellerId(string sellerId)
        {
            return _ctx.PointOfSales.Any(u => u.DtClose == null && u.Seller.Id == sellerId);
        }

        public bool OpenPOSExistsById(string pointOfSaleId)
        {
            return _ctx.PointOfSales.Any(u => u.DtClose == null && u.Id == pointOfSaleId);
        }
    }
}
