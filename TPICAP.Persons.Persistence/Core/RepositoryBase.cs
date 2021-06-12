using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPICAP.Persons.Domain;
using TPICAP.Persons.Domain.Core;

namespace TPICAP.Persons.Persistence
{
    public abstract class RepositoryBase<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : Entity
        where TDbContext : DbContext
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly DbContext _dbContext;

        public RepositoryBase(TDbContext entities)
        {
            _dbContext = entities;
            _dbSet = entities.Set<TEntity>();
        }

        public void Add(TEntity entityToAdd)
        {
            _dbSet.Add(entityToAdd);
        }

        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
        }

        public void Delete(TEntity entityToDelete)
        {
            _dbSet.Remove(entityToDelete);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
