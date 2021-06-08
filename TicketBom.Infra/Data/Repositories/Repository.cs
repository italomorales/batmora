using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TicketBom.Domain;
using TicketBom.Domain.Entities;

namespace TicketBom.Infra.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        
        protected TicketBomContext _ctx;
        protected DbSet<TEntity> _set;

        public Repository(TicketBomContext ctx)
        {
            _ctx = ctx;
            _set = _ctx.Set<TEntity>();
        }

        public virtual Task<TEntity> FindByIdAsync(string id)
        {
            return _set.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _set.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return _set.Update(entity).Entity;
        }

        public virtual void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }

        public EntityEntry Entry(object entity)
        {
            return _ctx.Entry(entity);
        }
    }
}
