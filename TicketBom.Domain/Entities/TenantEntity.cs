using System.Text.Json.Serialization;

namespace TicketBom.Domain.Entities
{
    public class TenantEntity : Entity
    {
        [JsonIgnore]
        public string TenantId { get; set; }
    }
}
