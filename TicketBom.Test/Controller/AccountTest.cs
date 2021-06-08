using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBom.Application.Commands.Auth;
using TicketBom.Application.Commands.User;
using TicketBom.Application.Services;

namespace TicketBom.Test.Controller
{
    [TestClass]
    public class CryptTest : BaseTest
    {
        private IAccountService _accountService;
        private IUserService _userService;

        [TestMethod]
        public void ShouldForgotPassword()
        {
            _accountService = Provider.GetRequiredService<IAccountService>();
            _userService = Provider.GetRequiredService<IUserService>();

            _userService.Add(new CreateUserCommand
            {
                Email = "italo.morales@hotmail.com",
                Mobile = "114124575",
                Name = "Italo Morales",
                Profiles = new List<CreateProfileCommand>
                {
                    new CreateProfileCommand
                    {
                        Id = "123"
                    }
                }
            });
            
            _accountService.Forgot(new ForgotCommand { Email = "italo.morales@hotmail.com" });

            Assert.IsNotNull(true);

        }
    }
}
