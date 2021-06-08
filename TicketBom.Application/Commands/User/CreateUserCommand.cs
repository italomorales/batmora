using System.Collections.Generic;
using Flunt.Validations;

namespace TicketBom.Application.Commands.User
{
    public class CreateUserCommand : Command
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<CreateProfileCommand> Profiles { get; set; }

        public override void Validate()
        {
            var contract = new Contract()
                .HasMinLen(Name, 3, "Name", "O nome deve ter no mínimo 3 caracteres")
                .HasMaxLen(Name, 100, "Name", "O nome deve ter no máximo 100 caracteres")
                .IsEmail(Email, "Email", "O email está inválido")
                .IsNotNull(Profiles, "Profile", "O perfil é obrigatório");

            AddNotifications(contract);
        }
    }
}
