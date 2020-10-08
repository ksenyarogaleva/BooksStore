using BooksStore.DAL.Interfaces;
using BooksStore.Models.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.Tests.BLL
{
    public class RepositoryTests
    {
        private Mock<IRepository<Author>> repository;
        private List<Author> authors;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRepository<Author>>();
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
                    Name = "Ben Albahari"
                });
            authors.Add(
                new Author
                {
                    Id = 3,
                    Name = "Sergey Teplyakov"
                });

        }

        [Test]
        public async Task ExistsTest()
        {
            //Act
            repository.Setup(a => a.ExistsAsync(
                It.IsAny<Expression<Func<Author, bool>>>()))
                .ReturnsAsync((Expression<Func<Author, bool>> predicate)=>
                {
                    return authors.Any(predicate.Compile());
                });

            //Arrange
            var mockRepository = repository.Object;
            var author1 = await mockRepository.ExistsAsync(a => a.Name.Equals("Jeffrey Rihter"));
            var author2 = await mockRepository.ExistsAsync(a => a.Name.Equals("jeffrey Rihter"));
            var author3 = await mockRepository.ExistsAsync(a => a.Id==1);
            var author4 = await mockRepository.ExistsAsync(a => a.Id==4);

            //Assert
            Assert.IsTrue(author1);
            Assert.IsFalse(author2);
            Assert.IsTrue(author3);
            Assert.IsFalse(author4);
        }
    }
}
