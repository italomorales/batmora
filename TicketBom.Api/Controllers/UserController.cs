using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBom.Application.Commands.User;
using TicketBom.Application.Services;
using TicketBom.Application.ViewModels;

namespace TicketBom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "USER.VIEW")]
        public Task<List<UserViewModel>> Get()
        {
            return _userService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "USER.VIEW")]
        public Task<UserViewModel> Get(string id)
        {
            return _userService.Get(id);
        }


        [HttpPost]
        [Authorize(Roles = "USER.INSERT")]
        public async Task<ActionResult> Post(CreateUserCommand userCommand)
        {
            var response = await _userService.Add(userCommand);

            if (!response.Valid) return BadRequest(response);
            
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "USER.UPDATE")]
        public async Task<ActionResult> Put(UpdateUserCommand userCommand)
        {
            var response = await _userService.Update(userCommand);

            if (!response.Valid) return BadRequest(response);

            return Ok(response);
        }
    }
}
