using System.Threading.Tasks;
using BooksStore.BLL.Interfaces;
using BooksStore.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService service;

        public AuthorsController(IAuthorsService authorsService)
        {
            service = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authors = await service.GetAllAsync();

            return Ok(new { authors });
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var author = await service.GetSingleAsync(id);
            return Ok(new { author });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorDTO author)
        {
            await service.CreateAuthorAsync(author);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await service.GetSingleAsync(id);
            if (book is null)
            {
                return NotFound();
            }

            await service.DeleteAuthorAsync(id);
            return Ok();
        }
    }
}
