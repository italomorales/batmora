using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBom.Domain.Entities.AccountAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        public bool EmailExists(string email);
        Task<List<User>> GetAll();
        Task<User> Get(string id);
        Task<User> GetByEmail(string forgotCommandEmail);
    }
}
