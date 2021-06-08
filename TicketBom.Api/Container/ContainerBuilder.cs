using Microsoft.Extensions.DependencyInjection;
using TicketBom.Application.Services;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data.Repositories;
using TicketBom.Infra.Email;
using TicketBom.Infra.User;

namespace TicketBom.Api.Container
{
    public static class ContainerBuilder
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPointOfSaleService, PointOfSaleService>();
            services.AddScoped<IPointOfSaleRepository, PointOfSaleRepository>();
            services.AddScoped<IFinancialEventService, FinancialEventService>();
            services.AddScoped<IFinancialEventRepository, FinancialEventRepository>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IItemSaleRepository, ItemSaleRepository>();
        }
    }
}
