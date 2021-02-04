using bookshelf.Model.Books;
using bookshelf.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.FakeData
{
    public class DataFake : IData
    {
        public List<Book> Books;
        public List<UserBook> UserBooks;

        public List<Book> GetAllBooks()
        {
            return CreateBooksFake();
        }

        private List<Book> CreateBooksFake()
        {
            Books = new List<Book>()
            {
                new Book {Id = new Guid(), Title = "Think and Grow Rich", Author = new Author {Id = new Guid(), FirstName="Napoleon",
                            LastName = "Hill"}, Genre = new Genre {Id = new Guid(), Name = "Self-development"}, Rating= 9 },
                new Book {Id = new Guid(), Title = "Witcher", Author = new Author {Id = new Guid(), FirstName="Andrzej",
                            LastName = "Sapkowski"}, Genre = new Genre {Id = new Guid(), Name = "Fantasy"}, Rating= 9 },
                new Book {Id = new Guid(), Title = "Solaris", Author = new Author {Id = new Guid(), FirstName="Stanisław",
                            LastName = "Lem"}, Genre = new Genre {Id = new Guid(), Name = "Sci-fi"}, Rating= 9 },
                new Book {Id = new Guid(), Title = "The Long Earth", Author = new Author {Id = new Guid(), FirstName="Terry",
                            LastName = "Pratchett"}, Genre = new Genre {Id = new Guid(), Name = "Fantasy"}, Rating= 8 },
                new Book {Id = new Guid(), Title = "Invincible", Author = new Author {Id = new Guid(), FirstName="Amy",
                            LastName = "Lawernce"}, Genre = new Genre {Id = new Guid(), Name = "Documentary"}, Rating= 9 },

            };
            return Books;
        }
        private List<UserBook> CreateFakeUserBooks()
        {
            User alonzo = new User()
            {
                Id = new Guid(),
                UserName = "Aloso Perez",
                Email = "Alozonzo@aaa.gmail.com",
                Password = "123456",
                IsPublic = true,
                IsActive = true,
                City = "Meksyk",
                PhotoPath = "aaaaaaaaaaa",
                RegistrationDate = new DateTime(2020, 11, 10)
            };

            UserBooks = new List<UserBook>()
            {
                new UserBook
                {
                    Id = new Guid(),
                    Book = new Book {Id = new Guid(), Title = "Invincible", Author = new Author {Id = new Guid(), FirstName="Amy",
                            LastName = "Lawernce"}, Genre = new Genre {Id = new Guid(), Name = "Documentary"}, Rating= 9 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },                
                new UserBook
                {
                    Id = new Guid(),
                    Book =  new Book {Id = new Guid(), Title = "Think and Grow Rich", Author = new Author {Id = new Guid(), FirstName="Napoleon",
                            LastName = "Hill"}, Genre = new Genre {Id = new Guid(), Name = "Self-development"}, Rating= 9 }
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },                
                new UserBook
                {
                    Id = new Guid(),
                    Book = new Book {Id = new Guid(), Title = "Witcher", Author = new Author {Id = new Guid(), FirstName="Andrzej",
                            LastName = "Sapkowski"}, Genre = new Genre {Id = new Guid(), Name = "Fantasy"}, Rating= 9 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },                
                new UserBook
                {
                    Id = new Guid(),
                    Book = new Book {Id = new Guid(), Title = "Solaris", Author = new Author {Id = new Guid(), FirstName="Stanisław",
                            LastName = "Lem"}, Genre = new Genre {Id = new Guid(), Name = "Sci-fi"}, Rating= 9 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },               
                new UserBook
                {
                    Id = new Guid(),
                    Book = new Book {Id = new Guid(), Title = "The Long Earth", Author = new Author {Id = new Guid(), FirstName="Terry",
                            LastName = "Pratchett"}, Genre = new Genre {Id = new Guid(), Name = "Fantasy"}, Rating= 8 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                }
            };
        }

    }
}
