using System;
using System.Collections.Generic;
using TicketBom.Domain.Entities.AccountAggregate;

namespace TicketBom.Domain.Entities.TicketAggregate
{
    public class PointOfSale : TenantEntity
    {
        public User Admin { get; private set; }
        public User Seller { get; set; }
        public DateTime DtOpen { get; private set; }
        public DateTime? DtClose { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public List<FinancialEvent> FinancialEvents { get; private set; }
        public PointOfSale(){}
        public PointOfSale(User seller, User admin)
        {
            DtOpen = DateTime.Now;
            Seller = seller;
            Admin = admin;
            FinancialEvents = new List<FinancialEvent>();
        }

        public PointOfSale(User seller, User admin, List<FinancialEvent> financialEvents)
        {
            DtOpen = DateTime.Now;
            Seller = seller;
            Admin = admin;
            FinancialEvents = financialEvents;
        }

        public void AddFinancialEvents(FinancialEvent financialEvent)
        {
            FinancialEvents.Add(financialEvent);
        }
    }
}
