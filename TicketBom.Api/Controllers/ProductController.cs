using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBom.Application.Commands.Product;
using TicketBom.Application.Services;
using TicketBom.Domain;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;

namespace TicketBom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly TicketBomContext _context;
        private readonly IProductService _productService; 

        public ProductController(TicketBomContext context, 
                              IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        [HttpGet]
        // [Authorize(Roles = "USER.VIEW")]
        public Task<List<Product>> Get()
        {
            return _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "USER.VIEW")]
        public Task<Product> Get(string id)
        {
            return _context.Products.FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }


        [HttpPost]
        // [Authorize(Roles = "USER.INSERT")]
        public async Task<ActionResult> Post(CreateProductCommand productCommand)
        {
            var response = await _productService.Add(productCommand);

            if (!response.Valid) return BadRequest(response);
            
            return Ok(response);
        }

        [HttpPut]
        // [Authorize(Roles = "USER.UPDATE")]
        public async Task<ActionResult> Put(UpdateProductCommand productCommand)
        {
            var response = await _productService.Update(productCommand);

            if (!response.Valid) return BadRequest(response);

            return Ok(response);
        }
    }
}
