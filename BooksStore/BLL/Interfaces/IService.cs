using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.BLL.Interfaces
{
    public interface IService<TEntity> where TEntity:class
    {
        bool Exists(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetSingle(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
