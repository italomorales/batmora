using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBom.Application.Commands.PointOfSale;
using TicketBom.Application.Services;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;

namespace TicketBom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PointOfSaleController : ControllerBase
    {
        private readonly TicketBomContext _context;
        private readonly IPointOfSaleService _pointOfSaleService; 

        public PointOfSaleController(TicketBomContext context, 
                              IPointOfSaleService pointOfSaleService)
        {
            _context = context;
            _pointOfSaleService = pointOfSaleService;
        }

        [HttpGet]
        [Authorize(Roles = "USER.VIEW")]
        public Task<List<PointOfSale>> Get()
        {
            return _context.PointOfSales.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "USER.VIEW")]
        public Task<PointOfSale> Get(string id)
        {
            return _context.PointOfSales.FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }


        [HttpPost]
        [Authorize(Roles = "USER.INSERT")]
        public async Task<ActionResult> Post(CreatePointOfSaleCommand pointofsaleCommand)
        {
            var response = await _pointOfSaleService.Add(pointofsaleCommand);

            if (!response.Valid) return BadRequest(response);
            
            return Created("", "");
        }
    }
}
