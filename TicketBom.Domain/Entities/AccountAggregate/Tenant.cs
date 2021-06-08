namespace TicketBom.Domain.Entities.AccountAggregate
{
    public class Tenant : Entity
    {
        protected Tenant() { }

        public Tenant(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
