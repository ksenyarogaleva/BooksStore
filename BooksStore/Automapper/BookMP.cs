using AutoMapper;
using BooksStore.Models;
using BooksStore.Models.DTO;

namespace BooksStore.Automapper
{
    public class BookMP:Profile
    {
        public BookMP()
        {
            CreateMap();
        }

        public void CreateMap()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(b => b.Author, opt => opt.Ignore());
        }
    }
}
