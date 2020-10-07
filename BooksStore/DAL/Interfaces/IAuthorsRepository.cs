using BooksStore.Models;
using System.Threading.Tasks;

namespace BooksStore.DAL.Interfaces
{
    public interface IAuthorsRepository:IRepository<Author>
    {
        Task<Author> GetAuthorByName(string name);
    }
}
