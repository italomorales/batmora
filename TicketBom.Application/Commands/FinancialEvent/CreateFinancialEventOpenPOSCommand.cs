using System;

namespace TicketBom.Application.Commands.FinancialEvent
{
    public class CreateFinancialEventOpenPosCommand
    {
        public string EventTypeId { get; set; }
        public decimal Amount { get; set; }
    }
}
