using System.Threading.Tasks;

namespace TicketBom.Domain.Entities
{
    public interface IRepository<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> FindByIdAsync(string id);
    }
}
