using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Infra.User;

namespace TicketBom.Infra.Data.Repositories
{
    public class UserRepository : Repository<Domain.Entities.AccountAggregate.User>, IUserRepository
    {
        private readonly IUserAccessor _userAccessor;
        public UserRepository(TicketBomContext ctx, 
                              IUserAccessor userAccessor) : base(ctx)
        {
            _userAccessor = userAccessor;
        }

        public bool EmailExists(string email)
        {
            return _ctx.Users.Any(u => u.Email == email);
        }

        public Task<List<Domain.Entities.AccountAggregate.User>> GetAll()
        {
            return _ctx.Users.Where(u => u.TenantId == _userAccessor.TenantId).ToListAsync();
        }

        public Task<Domain.Entities.AccountAggregate.User> Get(string id)
        {
            return _ctx.Users.Include(x => x.Profiles).FirstOrDefaultAsync(u => u.TenantId == _userAccessor.TenantId && u.Id == id);
        }

        public Task<Domain.Entities.AccountAggregate.User> GetByEmail(string email)
        {
            return _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
