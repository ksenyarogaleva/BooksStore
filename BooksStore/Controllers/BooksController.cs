using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksStore.DAL.Interfaces;
using BooksStore.Models;
using BooksStore.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly ILogger<BooksController> _logger;
        private readonly IUnitOfWork uow;

        public BooksController(ILogger<BooksController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.uow = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entities = await this.uow.Books.GetAllAsync();
            var books = new List<BookDTO>();
            foreach(var entity in entities)
            {

                var authors = new StringBuilder();
                    foreach (var author in entity.BookAuthors)
                    {
                        authors.Append(author.Author.Name);
                        authors.Append(';');
                    }

                books.Add(
                    new BookDTO { Id = entity.Id, Title = entity.Title, Definition = entity.Definition, Author = authors.ToString() });
            }

            return Ok(new { books });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await this.uow.Books.GetSingleAsync(id);
            var authors = new StringBuilder();
            foreach(var author in entity.BookAuthors)
            {
                authors.Append(author.Author.Name);
                authors.Append(';');
            }

            var book = new BookDTO { Id = entity.Id, Title = entity.Title, Definition = entity.Definition, Author = authors.ToString() };

            return Ok(new { book });
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookDTO book)
        {
            char[] separators = new char[] { ';', ',' };
            var authors=book.Author.Split(separators);
            foreach(var author in authors)
            {
                var exists = await this.uow.Authors.ExistsAsync(a => a.Name.ToUpper().Equals(author.ToUpper()));
                if (!exists)
                {

                }
            }
        }

      
    }
}
