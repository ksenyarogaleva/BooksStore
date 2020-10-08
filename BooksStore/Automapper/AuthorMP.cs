using AutoMapper;
using BooksStore.Models.Entities;
using BooksStore.Models.DTO;

namespace BooksStore.Automapper
{
    public class AuthorMP : Profile 
    {
        public AuthorMP()
        {
            CreateMap();
        }    

        private void CreateMap()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(a => a.Books, opt => opt.Ignore())
                .ReverseMap();
        }
                
    }
}
