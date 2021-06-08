using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TicketBom.Application.Commands.Auth;
using TicketBom.Application.Services;
using TicketBom.Infra.Crypto;
using TicketBom.Infra.Data;

namespace TicketBom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly TicketBomContext _context;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;


        public AuthController(TicketBomContext context, 
                              IUserService userService,
                              IAccountService accountService)
        {
            _context = context;
            _userService = userService;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginCommand loginCommand)
        {

            var userBase = await _context.Users.Include("Profiles.Roles").FirstOrDefaultAsync(u => u.Email == loginCommand.Email);

            if (userBase == null || userBase.Md5Pass != loginCommand.Password.ToMd5())
            {
                return Unauthorized("Login ou senha inválido");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("verystrongkeyverystrongkeyverystrongkey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, loginCommand.Email),
                    new Claim("TenantId", userBase.TenantId), 
                    new Claim("UserId", userBase.Id), 
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var claims = new List<Claim>();
            userBase.Profiles.ForEach(p => p.Roles.GroupBy(r => r.Key).ToList().ForEach(g =>
            {
                if (claims.All(c => c.Value != g.Key))
                    claims.Add(new Claim(ClaimTypes.Role, g.Key));
            }));

            tokenDescriptor.Subject.AddClaims(claims);


            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            // Retorna os dados
            return new
            {
                userBase,
                token = tokenHandler.WriteToken(token)
            };
        }


        [HttpPost]
        [Route("forgot")]
        public async Task<ActionResult<dynamic>> Forgot([FromBody] ForgotCommand forgotCommand)
        {
            var response = await _accountService.Forgot(forgotCommand);

            if (!response.Valid) return BadRequest(response);

            return Ok(response);
        }
    }
}
