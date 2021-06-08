using System;
using System.Collections.Generic;
using TicketBom.Application.Commands.FinancialEvent;

namespace TicketBom.Application.Commands.PointOfSale
{
    public class CreatePointOfSaleCommand : Command
    {
        public string SellerId { get; set; }
        public List<CreateFinancialEventOpenPosCommand> FinancialEvents { get; set; }
        public override void Validate(){}
    }
}
