using System;

namespace TicketBom.Domain.Entities.TicketAggregate
{
    public enum EnumStatusTicket { New, Printed, Used, Canceled}
    public class Ticket : TenantEntity
    {

        public ItemSale ItemSale { get; set; }
        public int Printed { get; set; }
        public DateTime Created { get; set; }
        public EnumStatusTicket StatusTicket { get; set; }
        public DateTime Used { get; set; }
        public string LocalUsed { get; set; }
        
        public Ticket() { }

        public Ticket(int printed, bool cortesy)
        {
            Printed = printed;
            Created = DateTime.Now;
            StatusTicket = EnumStatusTicket.New;
        }
    }
}
