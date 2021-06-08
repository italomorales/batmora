namespace TicketBom.Infra.User
{
    public interface IUserAccessor
    {
        string Id { get; set; }
        string TenantId { get; set; }
    }

    public class UserAccessor : IUserAccessor
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
    }

}
