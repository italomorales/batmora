using Flunt.Validations;

namespace TicketBom.Application.Commands.Auth
{
    public class ChangePassCommand : Command
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public override void Validate()
        {
            var contract = new Contract()
                .IsNotNullOrEmpty(Token, "Token", "O token deve estar preenchido")
                .IsNotNullOrEmpty(Password, "Password", "A senha é obrigatória")
                .IsTrue(Password != ConfirmPassword, "ConfirmPassword", "Os campos de senha devem ser iguais");

            AddNotifications(contract);
        }
    }
}
