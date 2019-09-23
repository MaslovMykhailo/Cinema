using Cinema.Persisted.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.BusinessLogic.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(Guid id, TEntity entity);
        Task<bool> RemoveAsync(Guid id);
        Task<TEntity> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
