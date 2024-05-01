using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Persistence.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<int> SaveChangesAsync();
        Task DeleteAllAsync(IEnumerable<T> entities);
        Task<bool> AnyAsync(params Expression<Func<T, object>>[] includeProperties);
    }
}
