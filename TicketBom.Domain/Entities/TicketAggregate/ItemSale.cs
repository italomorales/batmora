using TicketBom.Domain.Entities.AccountAggregate;

namespace TicketBom.Domain.Entities.TicketAggregate
{
    public class ItemSale : TenantEntity
    {

        public Sale Sale { get; set; }
        public Product Product { get; set; }
        public string DescriptionOfProduct { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Cortesy { get; set; }
        public User CortesyApprovedBy { get; set; }
        public bool Delete { get; set; }

        public ItemSale() { }

        public ItemSale(Product product, int quantity, bool cortesy, User cortesyApprovedBy)
        {
            Product = product;
            DescriptionOfProduct = product.Description;
            Price = product.Price;
            Quantity = quantity;
            Cortesy = cortesy;
            CortesyApprovedBy = cortesyApprovedBy;
            Validate();
        }

        private void Validate()
        {
            if(Cortesy)
                if(CortesyApprovedBy == null)
                    AddNotification("CortesyApprovedBy", "Quando o item for cortesia é necessario aprovação de um administrador.");
            
            if(Quantity <= 0)
                AddNotification("Quantity", "O item de venda deve ser maior que zero.");
            
            if(Product == null)
                AddNotification("Product", "O produto deve ser informado.");
            
        }
    }
}
