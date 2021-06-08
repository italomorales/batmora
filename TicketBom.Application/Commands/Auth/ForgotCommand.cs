using Flunt.Validations;

namespace TicketBom.Application.Commands.Auth
{
    public class ForgotCommand : Command
    {
        public string Email { get; set; }
        public override void Validate()
        {
            var contract = new Contract()
                .IsEmail(Email, "Email", "O email está inválido");

            AddNotifications(contract);
        }
    }
}
