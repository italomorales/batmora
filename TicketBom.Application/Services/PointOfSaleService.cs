using System.Collections.Generic;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using TicketBom.Application.Commands;
using TicketBom.Application.Commands.PointOfSale;
using TicketBom.Domain;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;
using TicketBom.Infra.User;

namespace TicketBom.Application.Services
{
    public class PointOfSaleService : IPointOfSaleService
    {

        private readonly TicketBomContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPointOfSaleRepository _pointfOfSaleRepository;
        private readonly IUserAccessor _userAccessor;

        public PointOfSaleService(TicketBomContext context, 
                                  IUserRepository userRepository,
                                  IPointOfSaleRepository pointOfSaleRepository, 
                                  IUserAccessor userAccessor)
        {
            _context = context;
            _userRepository = userRepository;
            _pointfOfSaleRepository = pointOfSaleRepository;
            _userAccessor = userAccessor;
        }

        public async Task<CommandResponse> Add(CreatePointOfSaleCommand createPointOfSaleCommand)
        {
            var seller = await _userRepository.FindByIdAsync(createPointOfSaleCommand.SellerId);
            if (seller == null) {
                return new CommandResponse(new List<Notification> {
                    new Notification("Vendendor", "Vendedor não encontrado")
                }, "Erro ao criar ponto de venda");
            }

            if (_pointfOfSaleRepository.OpenPOSExistsSellerId(createPointOfSaleCommand.SellerId)) {
                return new CommandResponse(new List<Notification> {
                    new Notification("Ponto de Venda", "Existe um ponto de venda aberto para o Vendedor informado.")
                }, "Erro ao criar ponto de venda");
            }

            var admin = await _userRepository.FindByIdAsync(_userAccessor.Id);
            var pos = new PointOfSale(seller, admin);
            
            foreach (var item in createPointOfSaleCommand.FinancialEvents)
            {
                var eventType = new EventType(item.EventTypeId);
                _context.Entry(eventType).State = EntityState.Unchanged;
                pos.AddFinancialEvents(new FinancialEvent(pos.Admin, item.Amount, eventType));
            }

            await _context.PointOfSales.AddAsync(pos);
            await _context.SaveChangesAsync();

            return new CommandResponse<PointOfSale>(null);
        }
    }

    public interface IPointOfSaleService
    {
        Task<CommandResponse> Add(CreatePointOfSaleCommand createPointOfSaleCommand);
    }
}
