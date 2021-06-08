using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBom.Application.Commands.Sale;
using TicketBom.Application.Services;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;

namespace TicketBom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly TicketBomContext _context;
        private readonly ISaleService _saleService; 

        public SaleController(TicketBomContext context, 
                              ISaleService saleService)
        {
            _context = context;
            _saleService = saleService;
        }

        [HttpGet]
        // [Authorize(Roles = "USER.VIEW")]
        public Task<List<Sale>> Get()
        {
            return _context.Sales.ToListAsync();
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "USER.VIEW")]
        public Task<Sale> Get(string id)
        {
            return _context.Sales.FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }


        [HttpPost]
        // [Authorize(Roles = "USER.INSERT")]
        public async Task<ActionResult> Post(CreateSaleCommand saleCommand)
        {
            var response = await _saleService.Add(saleCommand);

            if (!response.Valid) return BadRequest(response);
            
            return Ok(response);
        }
    }
}
