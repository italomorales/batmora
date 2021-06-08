using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBom.Api.Container;
using TicketBom.Infra.Data;

namespace TicketBom.Test
{
    [TestClass]
    public class BaseTest
    {
        protected ServiceProvider Provider { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddApplicationServices();

            serviceCollection.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                        .AddJsonFile("appsettings.json").Build());

            

            serviceCollection.AddDbContext<TicketBomContext>(opt => opt.UseInMemoryDatabase("TicketBom"));

            Provider = serviceCollection.BuildServiceProvider();
        }

    }
}
