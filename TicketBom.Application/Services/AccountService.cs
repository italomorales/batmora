using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.Extensions.Configuration;
using TicketBom.Application.Commands;
using TicketBom.Application.Commands.Auth;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Infra.Crypto;
using TicketBom.Infra.Data;
using TicketBom.Infra.Email;

namespace TicketBom.Application.Services
{
    public class AccountService : IAccountService
    {

        private readonly TicketBomContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountService(TicketBomContext context, 
                              IUserRepository userRepository, 
                              IEmailService emailService, 
                              IConfiguration configuration)
        {
            _context = context;
            _userRepository = userRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<CommandResponse> Register(RegisterCommand registerCommand)
        {
            registerCommand.Validate();

            if (registerCommand.Invalid) {
                return new CommandResponse(registerCommand.Notifications, "Erro ao registrar usuário");
            }

            if (_userRepository.EmailExists(registerCommand.Email))
            {
                return new CommandResponse(new List<Notification> {
                    new Notification("Email", "E-mail já cadastrado")
                }, "Erro ao registrar usuário");
            }

            var tenant = new Tenant(registerCommand.TenantName);

            var user = new User(registerCommand.Email, registerCommand.Email, tenant.Id);

            await _context.Users.AddAsync(user);
            await _context.Tenants.AddAsync(tenant);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<CommandResponse> Forgot(ForgotCommand forgotCommand)
        {
            forgotCommand.Validate();

            if (forgotCommand.Invalid) {
                return new CommandResponse(forgotCommand.Notifications, "Erro ao recuperar senha esquecida");
            }

            var user = await _userRepository.GetByEmail(forgotCommand.Email);

            if (user == null) return new CommandResponse<string>("Senha enviada para o e-mail cadastrado");

            try {
                _emailService.Send(user.Email, "Recuperação de senha", GetBodyForgetEmail(user.Email, user.Name));
            }
            catch {
                return new CommandResponse(new List<Notification> { new Notification("Email", "Falha ao enviar email")}, "Erro ao recuperar senha esquecida");
            }

            return new CommandResponse<string>("Senha enviada para o e-mail cadastrado");
        }

        private string GetBodyForgetEmail(string email, string name)
        {
            return File.ReadAllText(_configuration["Template:EmailForgot"])
                       .Replace("%%NAME%%", name)
                       .Replace("%%LINK%%", GenerateLink(email));
        }

        private string GenerateLink(string email)
        {
            return _configuration["SiteUrl"] + "validate&token=" + $"email={email},time={DateTime.Now}".EncryptUrl();
        }

    }

    public interface IAccountService
    {
        Task<CommandResponse> Register(RegisterCommand registerCommand);
        Task<CommandResponse> Forgot(ForgotCommand forgotCommand);
    }
}
