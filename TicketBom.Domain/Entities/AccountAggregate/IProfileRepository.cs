using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBom.Domain.Entities.AccountAggregate
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<List<Profile>> GetAll();
    }
}
