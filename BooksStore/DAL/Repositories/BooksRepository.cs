using BooksStore.DAL.Interfaces;
using BooksStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.DAL.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        protected readonly PostgreSqlContext context;
        public BooksRepository(PostgreSqlContext postgreSqlContext)
        {
            this.context = postgreSqlContext;
        }

        public Task CreateAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Book, bool>> predicate)
        {
            return await this.context.Books.AnyAsync(predicate);
        }

        public async  Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            return await this.context.Books.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await this.context.Books
                .Include(book => book.BookAuthors)
                .ThenInclude(bookAuthors => bookAuthors.Author)
                .ToListAsync();
        }

        public async Task<Book> GetSingleAsync(int id)
        {
            return await this.context.Books
                .Include(book => book.BookAuthors)
                .ThenInclude(bookAuthors => bookAuthors.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
