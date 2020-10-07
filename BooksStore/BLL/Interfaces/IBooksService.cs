using BooksStore.Models.DTO;
using System.Threading.Tasks;

namespace BooksStore.BLL.Interfaces
{
    public interface IBooksService:IService<BookDTO>
    {
        Task CreateBookAsync(BookDTO book);
        Task DeleteBookAsync(int id);
    }
}
