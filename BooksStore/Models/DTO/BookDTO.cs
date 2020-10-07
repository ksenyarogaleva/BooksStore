using System.Collections.Generic;

namespace BooksStore.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Definition { get; set; }
        public ICollection<string> Authors { get; set; }
        public BookDTO()
        {
            Authors = new List<string>();
        }
    }
}
