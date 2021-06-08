using System.Collections.Generic;
using Flunt.Validations;

namespace TicketBom.Application.Commands.User
{
    public class UpdateUserCommand : Command
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<UpdateProfileCommand> Profiles { get; set; }

        public override void Validate()
        {
            var contract = new Contract()
                .IsNotNull(Id, "Id", "O id não pode ser nulo")
                .HasMinLen(Name, 3, "Name", "O nome deve ter no mínimo 3 caracteres")
                .HasMaxLen(Name, 100, "Name", "O nome deve ter no máximo 100 caracteres")
                .IsEmail(Email, "Email", "O email está inválido")
                .IsLowerThan(Profiles.Count, 1, "Profile", "Deve haver pelo menos um perfil");

            AddNotifications(contract);
        }
    }
}
