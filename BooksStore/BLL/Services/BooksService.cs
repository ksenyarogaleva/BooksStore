using AutoMapper;
using BooksStore.BLL.Interfaces;
using BooksStore.DAL.Interfaces;
using BooksStore.Models;
using BooksStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.BLL.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public BooksService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.uow = unitOfWork;
            this.mapper = mapper;
        }
        public async Task CreateBookAsync(BookDTO book)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(BookDTO entity)
        {
            var doesExists= await this.uow.Books.ExistsAsync(b=>b.Id==entity.Id);
            return doesExists;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var entities = await this.uow.Books.GetAllAsync();
            var books = new List<BookDTO>();
            foreach (var entity in entities)
            {
                var book = mapper.Map<BookDTO>(entity);
                book.Author = this.GetAuthors(entity);
                books.Add(book);
            }

            return books;
        }

        public async Task<BookDTO> GetSingleAsync(int id)
        {
            var entity = await this.uow.Books.GetSingleAsync(id);
            var book = mapper.Map<BookDTO>(entity);
            book.Author = this.GetAuthors(entity);

            return book;
        }

        private string GetAuthors(Book book)
        {
            var authors = new StringBuilder();
            foreach (var author in book.BookAuthors)
            {
                authors.Append(author.Author.Name);
                authors.Append(';');
            }

            return authors.ToString();
        }
    }
}
