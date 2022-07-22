using PhoneBox.Entities.Base;
using System.Linq.Expressions;

namespace PhoneBox.Repositories
{
    public interface IGenericRepository<TEntity> 
        where TEntity : BaseEntity
    {
        Task<bool> AddAsync(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(int id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);

    }
}
