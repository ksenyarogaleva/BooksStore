using System.Threading.Tasks;
using BooksStore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly ILogger<BooksController> _logger;
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

    }
}
