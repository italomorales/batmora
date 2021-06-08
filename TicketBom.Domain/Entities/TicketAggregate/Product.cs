using System;
using TicketBom.Domain.Entities.AccountAggregate;

namespace TicketBom.Domain.Entities.TicketAggregate
{
    public class Product : TenantEntity
    {
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int HoursToExpire { get; private set; }
        public User Admin { get; private set; }
        public DateTime DtCreated { get; private set; }
        public bool Active { get; private set; }

        public Product(){}

        public Product(string description, decimal price, int hoursToExpire, User admin)
        {
            Description = description;
            Price = price;
            DtCreated = DateTime.Now;
            Active = true;
            Admin = admin;
            HoursToExpire = hoursToExpire;
            Validate();
        }

        public void ChangeProduct(string description, decimal price, int hoursToExpire)
        {
            Description = description;
            Price = price;
            HoursToExpire = hoursToExpire;
            Validate();
        }

        public void Validate()
        {
            if(Price <= 0 )
                AddNotification("Price", "Preço do produto deve ser maior que zero.");

            if(HoursToExpire <= 0 )
                AddNotification("HoursToExpire", "O produto deve ter um período expiração.");

            if(Admin == null)
                AddNotification("Admin", "Deve ser informado um administrador.");
            
            if(string.IsNullOrEmpty(Description))
                AddNotification("description", "Descrição do produto é obrigatória.");
        }

        public void Activate(){
            Active = true;
        }
        public void Disable(){
            Active = false;
        }
       
    }
}
