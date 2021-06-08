using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBom.Application.Commands.FinancialEvent;
using TicketBom.Application.Services;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;

namespace TicketBom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialEventController : ControllerBase
    {
        private readonly TicketBomContext _context;
        private readonly IFinancialEventService _financialEventService; 

        public FinancialEventController(TicketBomContext context, 
                              IFinancialEventService financialEventService)
        {
            _context = context;
            _financialEventService = financialEventService;
        }

        [HttpGet]
        [Authorize(Roles = "USER.VIEW")]
        public Task<List<FinancialEvent>> Get()
        {
            return _context.FinancialEvents.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "USER.VIEW")]
        public Task<FinancialEvent> Get(string id)
        {
            return _context.FinancialEvents.Include(f => f.Admin).FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }

        [HttpPost]
        [Authorize(Roles = "USER.INSERT")]
        public async Task<ActionResult> Post(CreateFinancialEventCommand financialEventCommand)
        {
            var response = await _financialEventService.Add(financialEventCommand);

            if (!response.Valid) return BadRequest(response);
            
            return Created("", "");
        }
    }
}
