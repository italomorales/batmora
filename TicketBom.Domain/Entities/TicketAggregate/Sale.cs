using System;
using System.Collections.Generic;

namespace TicketBom.Domain.Entities.TicketAggregate
{

    public enum EnumPaymentType { DebitCard, CreditCard, Cash}
    public enum EnumStatusSale { OK, Canceled}
    public class Sale : TenantEntity
    {
        public PointOfSale PointOfSale { get; set; }
        public DateTime DtSale { get; private set; }
        public string ClientIdentification {get; set;}
        public EnumPaymentType PaymentType { get; set; }
        public decimal ValueReceived { get; set; }
        public decimal Discount { get; set; }
        public List<ItemSale> ItemsSale { get; set; }
        public EnumStatusSale StatusSale { get; set; }
        public Sale(){}

        public Sale(string clientIdentification, EnumPaymentType paymentType, decimal valueReceived, decimal discount, List<ItemSale> itemsSale)
        {
            DtSale = DateTime.Now;
            ClientIdentification = clientIdentification;
            PaymentType = paymentType;
            ValueReceived = valueReceived;
            Discount = discount;
            ItemsSale = itemsSale;
            StatusSale = EnumStatusSale.OK;
        } 

        public void Cancel(){
            StatusSale = EnumStatusSale.Canceled;
        }

        public void Validate()
        {
            if(ItemsSale.Count <= 0)
                AddNotification("ItemsSale", "Deve haver ao menos um item na venda.");
            
            if(ValueReceived < 0)
                AddNotification("ValueReceived", "O valor de venda deve ser positivo.");

            if(Discount < 0)
                AddNotification("Discount", "O valor de desconto deve ser positivo.");
            
            if(Discount > 100)
                AddNotification("Discount", "O valor de desconto deve ser no máximo de 100%.");
        }
    }
}
