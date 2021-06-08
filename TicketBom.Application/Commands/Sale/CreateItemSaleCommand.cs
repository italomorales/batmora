using System;
using System.Collections.Generic;
using Flunt.Validations;
using TicketBom.Domain.Entities.TicketAggregate;

namespace TicketBom.Application.Commands.Sale
{
    public class CreateItemSaleCommand : Command
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Cortesy { get; set; }
        public string CortesyApprovedById { get; set; }

        public override void Validate()
        {
            var contract = new Contract()
                .IsTrue(Cortesy && !string.IsNullOrEmpty(CortesyApprovedById), "Cortesy", "Cortesia deve ter uma aprovador.")
                .IsNullOrEmpty(ProductId, "ProductId", "O produto deve ser informado.")
                .IsLowerThan(Quantity, 0, "Quantity", "Quantidade deve ser maior que zero.");

            AddNotifications(contract);
        }
    }
}
