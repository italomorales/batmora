using Flunt.Validations;

namespace TicketBom.Application.Commands.Product
{
    public class CreateProductCommand : Command
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }
        public int HoursToExpire { get; set; }

        public override void Validate()
        {
            var contract = new Contract()
                .HasMinLen(Description, 3, "Description", "A descrição deve ter no mínimo 3 caracteres")
                .HasMaxLen(Description, 100, "Description", "A descrição deve ter no máximo 100 caracteres")
                .IsLowerThan(Price, 0, "Price", "O preço do produto deve ser maior que zero.")
                .IsLowerThan(HoursToExpire, 0, "HoursToExpire", "O produto deve ter um período expiração.");

            AddNotifications(contract);
        }
    }
}
