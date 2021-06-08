namespace TicketBom.Domain.Entities.TicketAggregate
{
    public interface IPointOfSaleRepository : IRepository<PointOfSale>
    {
        bool OpenPOSExistsSellerId(string sellerId);
        bool OpenPOSExistsById(string pointOfSaleId);
    }
}
