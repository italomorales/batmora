using System.Collections.Generic;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using TicketBom.Application.Commands;
using TicketBom.Application.Commands.FinancialEvent;
using TicketBom.Domain;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;
using TicketBom.Infra.User;

namespace TicketBom.Application.Services
{
    public class FinancialEventService : IFinancialEventService
    {

        private readonly TicketBomContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPointOfSaleRepository _pointfOfSaleRepository;
        private readonly IUserAccessor _userAccessor;

        public FinancialEventService(TicketBomContext context,
                                     IUserRepository userRepository,
                                     IPointOfSaleRepository pointOfSaleRepository, 
                                     IUserAccessor userAccessor)
        {
            _context = context;
            _userRepository = userRepository;
            _pointfOfSaleRepository = pointOfSaleRepository;
            _userAccessor = userAccessor;
        }

        public async Task<CommandResponse> Add(CreateFinancialEventCommand createFinancialEventCommand)
        {
            var pos = await _pointfOfSaleRepository.FindByIdAsync(createFinancialEventCommand.PointOfSaleId);

            if (pos.DtClose != null) {
                return new CommandResponse(new List<Notification> {
                    new Notification("Lançamento Financeiro", "Somente é permitido lançamento financeiro em ponto de venda aberto!")
                }, "Erro ao criar Lançamento Financeiro");
            }

            var admin = await _userRepository.FindByIdAsync(_userAccessor.Id);
            var eventType = new EventType(createFinancialEventCommand.EventTypeId);
            _context.Entry(eventType).State = EntityState.Unchanged;
            
            await _context.FinancialEvents.AddAsync(new FinancialEvent(pos, admin, createFinancialEventCommand.Amount, eventType));
            await _context.SaveChangesAsync();

            return new CommandResponse<FinancialEvent>(null);
        }
    }

    public interface IFinancialEventService
    {
        Task<CommandResponse> Add(CreateFinancialEventCommand createFinancialEventCommand);
    }
}
