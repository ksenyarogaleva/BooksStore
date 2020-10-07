using AutoMapper;

namespace BooksStore.Automapper
{
    public static class AutomapperConfiguration
    {
        public static IMapper GetMapperConfiguration()
        {
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile(new BookMP());
                x.AddProfile(new AuthorMP());
            });

            return config.CreateMapper();
        }
    }
}
