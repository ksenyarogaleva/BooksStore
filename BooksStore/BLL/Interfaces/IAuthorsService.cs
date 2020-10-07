using BooksStore.Models.DTO;
using System.Threading.Tasks;

namespace BooksStore.BLL.Interfaces
{
    public interface IAuthorsService:IService<AuthorDTO>
    {
        Task CreateAuthorAsync(AuthorDTO author);
        Task DeleteAuthorAsync(int id);

    }
}
