using BooksStore.DAL;
using BooksStore.DAL.Interfaces;
using BooksStore.Models;
using BooksStore.Tests.BLL;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Tests.Bll.Tests
{
    public class AuthorsRepositoryTests
    {
        private Mock<IAuthorsRepository> authorsRepository;
        private List<Author> authors;

        [SetUp]
        public void Setup()
        {
            authorsRepository = new Mock<IAuthorsRepository>();
            authors = new List<Author>();
            authors.Add(
                new Author
                {
                    Id = 1,
                    Name = "Jeffrey Rihter"
                });
            authors.Add(
                new Author
                {
                    Id = 2,
                    Name="Ben Albahari"
                });
            authors.Add(
                new Author
                {
                    Id=3,
                    Name="Sergey Teplyakov"
                });
                
        }

        [Test]
        public async Task GetAllTest()
        {
            //Act
            authorsRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(authors.AsEnumerable());

            //Arrange
            var mockRepository = authorsRepository.Object;
            var authorsList = await mockRepository.GetAllAsync();

            //Assert
            Assert.IsTrue(authorsList.Count() == 3);
        }

        [Test]
        public async Task GetSingleTest()
        {
            //Act
            authorsRepository.Setup(a => a.GetSingleAsync(It.IsAny<int>()))
                .ReturnsAsync((int i)=>
                {
                    return authors.FirstOrDefault(a => a.Id == i);
                });

            //Arrange
            var mockRepository = authorsRepository.Object;
            var author1 = await mockRepository.GetSingleAsync(1);
            var author2 = await mockRepository.GetSingleAsync(4);

            //Assert
            Assert.IsTrue(author1.Name.Equals("Jeffrey Rihter"));
            Assert.IsNull(author2);
        }

        [Test]
        public async Task GetAuthorByNameTest()
        {
            //Act
            authorsRepository.Setup(a => a.GetAuthorByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) =>
                {
                    return authors.FirstOrDefault(a => a.Name.ToUpper().Equals(name.ToUpper()));
                });

            //Arrange
            var mockRepository = authorsRepository.Object;
            var author1 = await mockRepository.GetAuthorByNameAsync("Jeffrey Rihter");
            var author2 = await mockRepository.GetAuthorByNameAsync("jeffrey rihter");

            //Assert
            Assert.IsTrue(author1.Name.ToUpper().Equals("JEFFREY RIHTER"));
            Assert.IsTrue(author2.Name.ToUpper().Equals("JEFFREY RIHTER"));
        }
    }
}