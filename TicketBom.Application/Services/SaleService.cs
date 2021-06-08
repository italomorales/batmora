using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using TicketBom.Application.Commands;
using TicketBom.Application.Commands.Sale;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;
using TicketBom.Infra.User;

namespace TicketBom.Application.Services
{
    public class SaleService : ISaleService
    {

        private readonly TicketBomContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IPointOfSaleRepository _pointOfSaleRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserAccessor _userAccessor;

        public SaleService(TicketBomContext context,
                           IProductRepository productRepository,
                           IPointOfSaleRepository pointOfSaleRepository,
                           ISaleRepository saleRepository,
                           IUserRepository userRepository,
                           IUserAccessor userAccessor)
        {
            _context = context;
            _pointOfSaleRepository = pointOfSaleRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _userRepository = userRepository;
            _userAccessor = userAccessor;
        }

        public async Task<CommandResponse> Add(CreateSaleCommand createSaleCommand)
        {
            createSaleCommand.Validate();
            
            if (createSaleCommand.Invalid) {
                return new CommandResponse(createSaleCommand.Notifications, "Erro ao incluir produto");
            }

            if (!_pointOfSaleRepository.OpenPOSExistsById(createSaleCommand.PointOfSaleId)) {
                return new CommandResponse(new List<Notification> {
                    new Notification("PointOfSale", "Ponto de Venda inexistente.")
                }, "Erro ao incluir produto");
            }

            var itemsSale = new List<ItemSale>();
            
            createSaleCommand.ItemsSale.ForEach(async i => {
                var product = await _productRepository.FindByIdAsync(i.ProductId);
                var itemSale = new ItemSale(product, i.Quantity, i.Cortesy, new User{ Id = i.CortesyApprovedById } );
                itemsSale.Add(itemSale);
            });

            var sale = new Sale(createSaleCommand.ClientIdentification, (EnumPaymentType) createSaleCommand.PaymentTypeId, createSaleCommand.ValueReceived, createSaleCommand.Discount, itemsSale);

            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();

            return new CommandResponse<Sale>(sale);
        }

    }

    public interface ISaleService
    {
        Task<CommandResponse> Add(CreateSaleCommand createSaleCommand);
    }
}
