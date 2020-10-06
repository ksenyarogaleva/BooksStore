using System;

namespace BooksStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorsRepository Authors { get; }
        IBooksRepository Books { get; }
    }
}
