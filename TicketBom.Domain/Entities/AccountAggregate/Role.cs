using System.Collections.Generic;

namespace TicketBom.Domain.Entities.AccountAggregate
{
    public class Role : Entity
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}
