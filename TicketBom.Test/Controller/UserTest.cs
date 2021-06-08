using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBom.Application.Commands.User;
using TicketBom.Application.Services;

namespace TicketBom.Test.Controller
{
    [TestClass]
    public class UserTest : BaseTest
    {
        private IUserService _userService;
       

        [TestMethod]
        public void ShouldAddAndGetUser()
        {
            _userService = Provider.GetRequiredService<IUserService>();

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

            user = _userService.Get(user.Data.Id).Result;

            Assert.IsNotNull(user);

        }
    }
}
