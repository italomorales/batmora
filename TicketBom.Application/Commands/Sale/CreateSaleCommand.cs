using System.Collections.Generic;
using Flunt.Validations;

namespace TicketBom.Application.Commands.Sale
{
    public class CreateSaleCommand : Command
    {
        public string PointOfSaleId { get; set; }
        public string ClientIdentification {get; set;}
        public int PaymentTypeId { get; set; }
        public decimal ValueReceived { get; set; }
        public decimal Discount { get; set; }
        public List<CreateItemSaleCommand> ItemsSale { get; set; }

        public override void Validate()
        {
            var contract = new Contract()
                .IsLowerThan(0, ItemsSale.Count, "ItemsSale", "A venda deve ter ao menos um item adicionado.")
                .IsLowerThan(0, ValueReceived, "ValueReceived", "O valor de venda deve ser positivo.")
                .IsLowerThan(0, Discount, "Discount", "O valor de desconto deve ser positivo.");
            
            ItemsSale.ForEach(x => x.Validate());

            AddNotifications(contract);
        }
    }
}
