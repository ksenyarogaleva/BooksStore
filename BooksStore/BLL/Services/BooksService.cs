﻿using AutoMapper;
using BooksStore.BLL.Interfaces;
using BooksStore.DAL.Interfaces;
using BooksStore.Models.Entities;
using BooksStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var entity = mapper.Map<Book>(book);
            entity.BookAuthors = new List<BookAuthor>();

            var doesBookExists = await uow.Books.ExistsAsync(b => b.Title.ToUpper().Equals(book.Title.ToUpper()));
           
            if (!doesBookExists)
            {
                foreach(var author in book.Authors){
                    var authorEntity = await uow.Authors.GetAuthorByNameAsync(author);

                    //TODO: add here a condition if there is no such author in db
                    var bookAuthor = new BookAuthor
                    {
                        Book = entity,
                        Author = authorEntity,
                    };

                    entity.BookAuthors.Add(bookAuthor);
                }

                await uow.Books.CreateAsync(entity);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await uow.Books.GetSingleAsync(id);
            await uow.Books.DeleteAsync(book);
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
                book.Authors = this.GetAuthors(entity).ToList();
                books.Add(book);
            }

            return books;
        }

        public async Task<BookDTO> GetSingleAsync(int id)
        {
            var entity = await this.uow.Books.GetSingleAsync(id);
            var book = mapper.Map<BookDTO>(entity);
            book.Authors = this.GetAuthors(entity).ToList();

            return book;
        }

        private IEnumerable<string> GetAuthors(Book book)
        {
            var authors = new List<string>();
            foreach (var author in book.BookAuthors)
            {
                authors.Add(author.Author.Name);
            }

            return authors;
        }
    }
}
