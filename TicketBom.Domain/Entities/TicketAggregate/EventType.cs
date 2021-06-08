
namespace TicketBom.Domain.Entities.TicketAggregate
{
    public enum EnumDebitCredit { Debit, Credit }
    public class EventType : TenantEntity
    {
        public string Description { get; private set; }
        public EnumDebitCredit DebitCredit { get; private set; }

        public EventType(string id)
        {
            Id = id;
        }
    }
}
