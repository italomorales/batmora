using System;
using Flunt.Validations;

namespace TicketBom.Application.Commands.Product
{
    public class UpdateProductCommand : Command
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; private set; }
        public int HoursToExpire { get; private set; }

        

        public override void Validate()
        {
            var contract = new Contract()
                .IsNotNull(Id, "Id", "O id não pode ser nulo")
                .HasMinLen(Description, 3, "Description", "A descrição deve ter no mínimo 3 caracteres")
                .HasMaxLen(Description, 100, "Description", "A descrição deve ter no máximo 100 caracteres")
                .IsLowerThan(Price, 0, "Price", "O preço do produto deve ser maior que zero.")
                .IsLowerThan(HoursToExpire, 0, "Tempo de Expiração", "O produto deve ter um período expiração.");

            AddNotifications(contract);
        }
    }
}
