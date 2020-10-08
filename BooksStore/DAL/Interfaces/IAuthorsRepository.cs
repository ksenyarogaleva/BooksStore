using BooksStore.Models.Entities;
using System.Threading.Tasks;

namespace BooksStore.DAL.Interfaces
{
    public interface IAuthorsRepository:IRepository<Author>
    {
        Task CreateAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
        Task<Author> GetAuthorByNameAsync(string name);
    }
}
