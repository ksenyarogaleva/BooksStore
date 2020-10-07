using BooksStore.DAL.Interfaces;
using BooksStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.DAL.Repositories
{
    public class AuthorsRepository:IAuthorsRepository
    {
        private readonly PostgreSqlContext context;

        public AuthorsRepository(PostgreSqlContext postgreSqlContext)
        {
            this.context = postgreSqlContext;
        }

        public async Task CreateAuthorAsync(Author author)
        {
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(Author author)
        {
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Author, bool>> predicate)
        {
            return await this.context.Authors.AnyAsync(predicate);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await this.context.Authors
                   .Include(author => author.BookAuthors)
                   .ThenInclude(bookAuthors => bookAuthors.Book)
                   .ToListAsync();
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await this.context.Authors
                .Include(author => author.BookAuthors)
                .ThenInclude(bookAuthor => bookAuthor.Book)
                .FirstOrDefaultAsync(a => a.Name.ToUpper().Equals(name.ToUpper()));
        }

        public async Task<Author> GetSingleAsync(int id)
        {
            return await this.context.Authors
                .Include(author => author.BookAuthors)
                .ThenInclude(bookAuthors => bookAuthors.Book)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
