using System.Collections.Generic;

namespace BooksStore.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Books { get; set; }
        public AuthorDTO()
        {
            Books = new List<string>();
        }
    }
}
