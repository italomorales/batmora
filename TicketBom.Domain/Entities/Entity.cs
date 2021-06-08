using System;
using Flunt.Notifications;

namespace TicketBom.Domain.Entities
{
    public class Entity : Notifiable
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
