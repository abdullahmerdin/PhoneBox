using PhoneBox.Entities.Base;
using System.Linq.Expressions;

namespace PhoneBox.Repositories
{
    public interface IGenericRepository<TEntity> 
        where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(int id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity? Get(Expression<Func<TEntity, bool>> filter);

    }
}
