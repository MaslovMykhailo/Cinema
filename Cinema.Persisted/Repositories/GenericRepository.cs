using Cinema.Persisted.Context;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Persisted.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly CinemaContext _context;
        private DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public GenericRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;

        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await _dbSet.AsQueryable().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);

            return true;
        }

        public async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Update(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }
    }
}
