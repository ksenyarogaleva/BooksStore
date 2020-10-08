using AutoMapper;
using BooksStore.BLL.Interfaces;
using BooksStore.DAL.Interfaces;
using BooksStore.Models.Entities;
using BooksStore.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.BLL.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public AuthorsService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.uow = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAuthorAsync(AuthorDTO author)
        {
            var entity = mapper.Map<Author>(author);
            entity.BookAuthors = new List<BookAuthor>();

            var doesAuthorExists = await uow.Authors.ExistsAsync(a => a.Name.ToUpper().Equals(entity.Name.ToUpper()));

            if (!doesAuthorExists)
            {
                foreach(var book in author.Books)
                {
                    var bookEntity = await uow.Books.GetBookByTitleAsync(book);
                    //TODO: add here a condition if there is no such book in db
                    var bookAuthor = new BookAuthor
                    {
                        Book = bookEntity,
                        Author = entity,
                    };

                    entity.BookAuthors.Add(bookAuthor);
                }

                await uow.Authors.CreateAuthorAsync(entity);
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await uow.Authors.GetSingleAsync(id);
            await uow.Authors.DeleteAuthorAsync(author);
        }

        public async Task<bool> ExistsAsync(AuthorDTO entity)
        {
            var doesExists = await uow.Authors.ExistsAsync(a => a.Id == entity.Id);
            return doesExists;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var entities = await uow.Authors.GetAllAsync();
            var authors = new List<AuthorDTO>();
            foreach(var entity in entities)
            {
                var author = mapper.Map<AuthorDTO>(entity);
                author.Books = this.GetBooks(entity).ToList();
                authors.Add(author);
            }

            return authors;
        }

        public async Task<AuthorDTO> GetSingleAsync(int id)
        {
            var entity = await uow.Authors.GetSingleAsync(id);
            var author = mapper.Map<AuthorDTO>(entity);
            author.Books = this.GetBooks(entity).ToList();

            return author;
        }

        private IEnumerable<string> GetBooks(Author author)
        {
            var books = new List<string>();
            foreach(var book in author.BookAuthors)
            {
                books.Add(book.Book.Title);
            }

            return books;
        }
    }
}
