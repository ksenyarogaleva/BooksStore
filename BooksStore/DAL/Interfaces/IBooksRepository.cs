using BooksStore.Models;
using System.Threading.Tasks;

namespace BooksStore.DAL.Interfaces
{
    public interface IBooksRepository:IRepository<Book>
    {
        Task CreateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<Book> GetBookByTitleAsync(string title);
    }
}
