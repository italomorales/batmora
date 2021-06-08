using System;
using TicketBom.Domain.Entities.AccountAggregate;

namespace TicketBom.Domain.Entities.TicketAggregate
{
    public class FinancialEvent : TenantEntity
    {
        public User Admin {get; set;}
        public DateTime DtCreated { get; set; }
        public decimal Amount { get; set; }
        public PointOfSale PointOfSale { get; set; }
        public EventType EventType {get; set;}

        public FinancialEvent(){}

        public FinancialEvent(User admin, decimal amount, EventType eventType)
        {
            DtCreated = DateTime.Now;
            Admin = admin;
            Amount = amount;
            EventType =  eventType;
        }
        public FinancialEvent(PointOfSale pointOfSale, User admin, decimal amount, EventType eventType)
        {
            DtCreated = DateTime.Now;
            PointOfSale = pointOfSale;
            Admin = admin;
            Amount = amount;
            EventType =  eventType;
        }

    }
}
