using BooksStore.BLL.Interfaces;
using BooksStore.DAL.Interfaces;
using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.BLL.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork uow;
        public BooksService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }
        public void CreateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Book entity)
        {
            var doesExists= Task.Run(async () => await this.uow.Books.ExistsAsync(b=>b.Id==entity.Id)).Result;
            return doesExists;
        }

        public  IEnumerable<Book> Find(Expression<Func<Book, bool>> predicate)
        {
            var books= Task.Run(async () => await this.uow.Books.FindAsync(predicate)).Result;
            return books;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = Task.Run(async () => await this.uow.Books.GetAllAsync()).Result;
            return books;
        }

        public Book GetSingle(int id)
        {
            var book = Task.Run(async () => await this.uow.Books.GetSingleAsync(id)).Result;
            return book;
        }
    }
}
