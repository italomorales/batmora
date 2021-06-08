using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBom.Application.Commands.PointOfSale;
using TicketBom.Application.Commands.Sale;
using TicketBom.Application.Commands.User;
using TicketBom.Application.Services;

namespace TicketBom.Test.Controller
{
    [TestClass]
    public class SaleTest : BaseTest
    {
        private IUserService _userService;
        private ISaleService _saleService;
        private IPointOfSaleService _pointOfSaleService;
        
        [TestMethod]
        public void ShouldAddAndGetSale()
        {
            _userService = Provider.GetRequiredService<IUserService>();
            _saleService = Provider.GetRequiredService<ISaleService>();
            _pointOfSaleService = Provider.GetRequiredService<IPointOfSaleService>();

            dynamic user = _userService.Add(new CreateUserCommand
            {
                Email = "italo@gmail.com",
                Mobile = "114124575",
                Name = "Italo Morales",
                Profiles = new List<CreateProfileCommand>
                {
                    new CreateProfileCommand
                    {
                        Id = "123"
                    }
                }
            }).Result;

            var pointOfSale = _pointOfSaleService.Add(new CreatePointOfSaleCommand{SellerId = user.Data.Id, FinancialEvents = null});

            

            Assert.IsNotNull(user);

        }
    }
}
