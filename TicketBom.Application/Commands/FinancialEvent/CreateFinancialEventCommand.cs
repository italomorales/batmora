namespace TicketBom.Application.Commands.FinancialEvent
{
    public class CreateFinancialEventCommand
    {
        public string PointOfSaleId { get; set; }
        public string EventTypeId { get; set; }
        public decimal Amount { get; set; }
    }
}
