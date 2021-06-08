using System.Collections.Generic;
using System.Threading.Tasks;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Infra.User;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TicketBom.Infra.Data.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        private readonly IUserAccessor _userAccessor;
        public ProfileRepository(TicketBomContext ctx, 
                              IUserAccessor userAccessor) : base(ctx)
        {
            _userAccessor = userAccessor;
        }

        public Task<List<Profile>> GetAll()
        {
            return _ctx.Profiles.Where(u => u.TenantId == _userAccessor.TenantId).ToListAsync();
        }
    }
}
