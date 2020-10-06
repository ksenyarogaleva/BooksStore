using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.BLL.Interfaces
{
    public interface IBooksService:IService<Book>
    {
        void CreateBook(Book book);
        void DeleteBook(int id);
    }
}
