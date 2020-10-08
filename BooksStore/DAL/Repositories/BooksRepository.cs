using BooksStore.DAL.Interfaces;
using BooksStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task CreateAsync(Book book)
        {
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Book, bool>> predicate)
        {
            return await this.context.Books.AnyAsync(predicate);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await this.context.Books
                .Include(book => book.BookAuthors)
                .ThenInclude(bookAuthors => bookAuthors.Author)
                .ToListAsync();
        }

        public async Task<Book> GetBookByTitleAsync(string title)
        {
            return await this.context.Books
                   .Include(book => book.BookAuthors)
                   .ThenInclude(bookAuthor => bookAuthor.Author)
                   .FirstOrDefaultAsync(a => a.Title.ToUpper().Equals(title.ToUpper()));
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
