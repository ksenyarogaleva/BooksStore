using System.Threading.Tasks;
using BooksStore.BLL.Interfaces;
using BooksStore.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IBooksService service;
        public BooksController(IBooksService booksService)
        {
            this.service = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await service.GetAllAsync();

            return Ok(new { books });
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await service.GetSingleAsync(id);
            return Ok(new { book });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDTO book)
        {
            await service.CreateBookAsync(book);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await service.GetSingleAsync(id);
            if(book is null)
            {
                return NotFound();
            }

            await service.DeleteBookAsync(id);
            return Ok();
        }
    }
}
