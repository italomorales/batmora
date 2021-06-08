using System.Collections.Generic;
using Flunt.Notifications;

namespace TicketBom.Domain.Entities.AccountAggregate
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Md5Pass { get; set; }
        public string Mobile { get; private set; }
        public string TenantId { get; set; }

        public List<Profile> Profiles { get; set; }

        public User(){}

        public User(string name, string email, string tenantId)
        {
            Name = name;
            Email = email;
            TenantId = tenantId;

            Validate();
        }

        public User(string name, string email, string mobile, string tenantId)
        {
            Name = name;
            Email = email;
            Mobile = mobile;
            TenantId = tenantId;
            
            Validate();
        }

        public void ChangeUser(string name, string email, string mobile, string tenantId)
        {
            Name = name;
            Email = email;
            Mobile = mobile;
            TenantId = tenantId;

            Validate();
        }

        private void Validate()
        {
            if(string.IsNullOrEmpty(Name))
                AddNotification(new Notification("Name", "Nome não pode ser vazio"));
        }
    }
}
