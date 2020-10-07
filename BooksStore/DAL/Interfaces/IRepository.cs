using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
