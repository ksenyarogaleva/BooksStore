using BooksStore.DAL.Interfaces;
using BooksStore.Models.Entities;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Tests.BLL
{
    public class BooksRepositoryTests
    {
        private Mock<IBooksRepository> booksRepository;
        private List<Book> books;

        [SetUp]
        public void Setup()
        {
            booksRepository = new Mock<IBooksRepository>();
            books = new List<Book>();
            books.Add(
                new Book
                {
                    Id = 1,
                    Title = "CLR via C#"
                });
            books.Add(
                new Book
                {
                    Id = 2,
                    Title = "C# 8.0 in a Nutshell:The Definitive ReferenceBen Albahari"
                });
            books.Add(
                new Book
                {
                    Id = 3,
                    Title = "C# 8.0 Pocket Reference:Instant Help for C# 8.0 Programmers"
                });

        }

        [Test]
        public async Task GetAllTest()
        {
            //Act
            booksRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(books.AsEnumerable());

            //Arrange
            var mockRepository = booksRepository.Object;
            var booksList = await mockRepository.GetAllAsync();

            //Assert
            Assert.IsTrue(booksList.Count() == 3);
        }

        [Test]
        public async Task GetSingleTest()
        {
            //Act
            booksRepository.Setup(a => a.GetSingleAsync(It.IsAny<int>()))
                .ReturnsAsync((int i) =>
                {
                    return books.FirstOrDefault(a => a.Id == i);
                });

            //Arrange
            var mockRepository = booksRepository.Object;
            var book1 = await mockRepository.GetSingleAsync(1);
            var book2 = await mockRepository.GetSingleAsync(4);

            //Assert
            Assert.IsTrue(book1.Title.Equals("CLR via C#"));
            Assert.IsNull(book2);
        }

        [Test]
        public async Task GetBookByTitleTest()
        {
            //Act
            booksRepository.Setup(a => a.GetBookByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync((string title) =>
                {
                    return books.FirstOrDefault(a => a.Title.ToUpper().Equals(title.ToUpper()));
                });

            //Arrange
            var mockRepository = booksRepository.Object;
            var author1 = await mockRepository.GetBookByTitleAsync("CLR via C#");
            var author2 = await mockRepository.GetBookByTitleAsync("clr via c#");

            //Assert
            Assert.IsTrue(author1.Title.ToUpper().Equals("CLR VIA C#"));
            Assert.IsTrue(author2.Title.ToUpper().Equals("CLR VIA C#"));
        }
    }
}
