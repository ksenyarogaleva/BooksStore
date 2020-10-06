using BooksStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly PostgreSqlContext context;
        public Repository(PostgreSqlContext postgreSqlContext)
        {
            this.context = postgreSqlContext;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(int id)
        {
            return await this.context.Set<TEntity>().FindAsync(id);
        }
    }
}
