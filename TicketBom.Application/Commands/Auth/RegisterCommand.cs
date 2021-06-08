using Flunt.Validations;

namespace TicketBom.Application.Commands.Auth
{
    public class RegisterCommand : Command
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string TenantName { get; set; }

        public override void Validate()
        {
            var contract = new Contract()
                .IsEmail(Email, "Email", "O email está inválido")
                .IsNull(Password, "Password", "A senha não pode ficar vazia")
                .IsNull(ConfirmPassword, "ConfirmPassword", "A confirmação da senha não pode ficar vazia")
                .IsNull(TenantName, "TenantName","O nome do cliente não pode ficar vazio")
                .IsNull(Email, "Email", "O email não é obrigatório");

            AddNotifications(contract);
        }
    }
}
