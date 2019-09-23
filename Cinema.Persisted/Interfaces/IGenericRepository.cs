using Cinema.Persisted.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cinema.Persisted.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAsync();

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<bool> RemoveAsync(Guid id);

        Task<TEntity> UpdateAsync(Guid id, TEntity entity);

        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> expression);
    }
}
