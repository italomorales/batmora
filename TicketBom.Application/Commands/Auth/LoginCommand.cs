using Flunt.Validations;

namespace TicketBom.Application.Commands.Auth
{
    public class LoginCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public override void Validate()
        {
            var contract = new Contract()
                .IsEmail(Email, "Email", "O email está inválido")
                .IsNotNull(Password, "Password", "A senha não pode ficar vazia");

            AddNotifications(contract);
        }
    }
}
