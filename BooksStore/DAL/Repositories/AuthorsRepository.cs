using BooksStore.DAL.Interfaces;
using BooksStore.Models;

namespace BooksStore.DAL.Repositories
{
    public class AuthorsRepository:Repository<Author>,IAuthorsRepository
    {
        public AuthorsRepository(PostgreSqlContext postgreSqlContext):base(postgreSqlContext)
        {

        }
    }
}
