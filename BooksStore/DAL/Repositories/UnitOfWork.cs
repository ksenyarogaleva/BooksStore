using BooksStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgreSqlContext context;
        public IAuthorsRepository Authors { get; private set; }

        public IBooksRepository Books { get; private set; }

        public UnitOfWork(PostgreSqlContext postgreSqlContext)
        {
            this.context = postgreSqlContext;
            Authors = new AuthorsRepository(this.context);
            Books = new BooksRepository(this.context);
        }
    }
}
