using System.Collections.Generic;

namespace TicketBom.Domain.Entities.AccountAggregate
{
    public class Profile : Entity
    {
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
        public string TenantId { get; set; }
    }
}
