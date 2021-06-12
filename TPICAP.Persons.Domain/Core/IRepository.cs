using System.Collections.Generic;
using System.Threading.Tasks;

namespace TPICAP.Persons.Domain.Core
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity aggregateToAdd);
        void Update(TEntity aggregateToUpdate);
        void Delete(TEntity aggregateToDelete);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
