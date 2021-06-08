using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.Extensions.Configuration;
using TicketBom.Application.Commands;
using TicketBom.Application.Commands.User;
using TicketBom.Application.ViewModels;
using TicketBom.Domain;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Infra.Crypto;
using TicketBom.Infra.Data;
using TicketBom.Infra.Email;
using TicketBom.Infra.User;

namespace TicketBom.Application.Services
{
    public class UserService : IUserService
    {

        private readonly TicketBomContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(TicketBomContext context, 
                           IUserRepository userRepository, 
                           IUserAccessor userAccessor,
                           IEmailService emailService,
                           IConfiguration configuration)
        {
            _context = context;
            _userRepository = userRepository;
            _userAccessor = userAccessor;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<CommandResponse> Add(CreateUserCommand createUserCommand)
        {
            createUserCommand.Validate();
            
            if (createUserCommand.Invalid) {
                return new CommandResponse(createUserCommand.Notifications, "Erro ao incluir usuário");
            }

            if (_userRepository.EmailExists(createUserCommand.Email)) {
                return new CommandResponse(new List<Notification> {
                    new Notification("Email", "E-mail já cadastrado")
                }, "Erro ao incluir usuário");
            }

            var user = new User(createUserCommand.Name, 
                                createUserCommand.Email, 
                                createUserCommand.Mobile,
                                _userAccessor.TenantId);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            try
            {
                _emailService.Send(user.Email, "Bem-vindo ao TiketBom", GetBodyWelcomeEmail(user.Name));
            }
            catch 
            {
                return new CommandResponse(new List<Notification> { new Notification("Email", "Falha ao enviar email") }, "Erro ao cadastrar cliente");
            }
            

            return new CommandResponse<User>(user);
        }

        public async Task<CommandResponse> Update(UpdateUserCommand updateUserCommand)
        {
            updateUserCommand.Validate();

            if (updateUserCommand.Invalid) {
                return new CommandResponse(updateUserCommand.Notifications, "Erro ao editar usuário");
            }

            var userBase = _context.Users.FirstOrDefault(u => u.Id == updateUserCommand.Id);

            if (userBase == null) {
                return  new CommandResponse(new List<Notification>
                {
                    new Notification("Id", "Usuário não cadastrado")
                }, "Erro ao editar usuario");
            }

            userBase.ChangeUser(updateUserCommand.Name, updateUserCommand.Email, updateUserCommand.Mobile, _userAccessor.TenantId);

            await _context.SaveChangesAsync();

            return new CommandResponse<User>(userBase);
        }

        public Task<List<UserViewModel>> GetAll()
        {
            var users = _userRepository.GetAll().Result;

            return Task.FromResult(users.Select(x => new UserViewModel
            {
                Id = x.Id, 
                Name = x.Name,
                Email = x.Email
            }).ToList());
        }

        public Task<UserViewModel> Get(string id)
        {
            var user = _userRepository.Get(id).Result;

            if (user == null) return Task.FromResult<UserViewModel>(null);

            return Task.FromResult(new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Profiles = user.Profiles.Select(profile =>
                    new ProfileViewModel { Description = profile.Description, Id = profile.Id }).ToList()
            });
        }

        private string GetBodyWelcomeEmail(string name)
        {
            return File.ReadAllText(_configuration["Template:EmailWelcome"])
                .Replace("%%NAME%%", name)
                .Replace("%%LINK%%", _configuration["SiteUrl"]);
        }
        
    }

    public interface IUserService
    {
        Task<CommandResponse> Add(CreateUserCommand createUserCommand);
        Task<CommandResponse> Update(UpdateUserCommand updateUserCommand);
        Task<List<UserViewModel>> GetAll();
        Task<UserViewModel> Get(string id);
    }
}
