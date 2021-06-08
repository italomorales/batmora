using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using TicketBom.Application.Commands;
using TicketBom.Application.Commands.Product;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data;
using TicketBom.Infra.User;

namespace TicketBom.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly TicketBomContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserAccessor _userAccessor;

        public ProductService(TicketBomContext context, 
                              IProductRepository productRepository,
                              IUserRepository userRepository,
                              IUserAccessor userAccessor)
        {
            _context = context;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _userAccessor = userAccessor;
        }

        public async Task<CommandResponse> Add(CreateProductCommand createProductCommand)
        {
            createProductCommand.Validate();
            
            if (createProductCommand.Invalid) {
                return new CommandResponse(createProductCommand.Notifications, "Erro ao incluir produto");
            }

            var admin = await _userRepository.FindByIdAsync(_userAccessor.Id);
            var product = new Product(createProductCommand.Description, createProductCommand.Price, createProductCommand.HoursToExpire, admin);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new CommandResponse<Product>(product);
        }

        public async Task<CommandResponse> Update(UpdateProductCommand updateProductCommand)
        {
            updateProductCommand.Validate();

            if (updateProductCommand.Invalid)
            {
                return new CommandResponse(updateProductCommand.Notifications, "Erro ao editar produto");
            }

            var productBase = _context.Products.FirstOrDefault(u => u.Id == updateProductCommand.Id);

            if (productBase == null) {
                return new CommandResponse(new List<Notification>
                {
                    new Notification("Id", "Produto não cadastrado")
                }, "Erro ao editar produto");
            }

            productBase.ChangeProduct(updateProductCommand.Description, updateProductCommand.Price, updateProductCommand.HoursToExpire);

            await _context.SaveChangesAsync();

            return new CommandResponse<Product>(productBase);
        }
    }

    public interface IProductService
    {
        Task<CommandResponse> Add(CreateProductCommand createProductCommand);
        Task<CommandResponse> Update(UpdateProductCommand updateProductCommand);
    }
}
