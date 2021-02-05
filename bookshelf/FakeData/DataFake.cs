using bookshelf.Model.Books;
using bookshelf.Model.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookshelf.DAL;

namespace bookshelf.FakeData
{
    public class DataFake : IBaseRepository<UserBook>
    {
        public List<UserBook> UserBooks;

        public DataFake()
        {
            User alonzo = new User()
            {
                Id = new Guid("667cd0ce-4d59-48e5-aba7-0c5e2432ddde"),
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
                    Id = new Guid("def9c864-7423-49f6-8ff6-5b96096c7341"),
                    Book = new Book {Id = Guid.NewGuid(), Title = "Invincible", Author = new Author {Id = Guid.NewGuid(), FirstName="Amy",
                            LastName = "Lawernce"}, Genre = new Genre {Id = Guid.NewGuid(), Name = "Documentary"}, Rating= 9 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },                
                new UserBook
                {
                    Id = new Guid("def9c865-7423-49f6-8ff6-5b96096c7341"),
                    Book =  new Book {Id = Guid.NewGuid(), Title = "Think and Grow Rich", Author = new Author {Id = Guid.NewGuid(), FirstName="Napoleon",
                            LastName = "Hill"}, Genre = new Genre {Id = Guid.NewGuid(), Name = "Self-development"}, Rating= 9 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },                
                new UserBook
                {
                    Id = new Guid("def9c864-7423-49f6-8ff6-5b96096c7346"),
                    Book = new Book {Id = Guid.NewGuid(), Title = "Witcher", Author = new Author {Id = Guid.NewGuid(), FirstName="Andrzej",
                            LastName = "Sapkowski"}, Genre = new Genre {Id = Guid.NewGuid(), Name = "Fantasy"}, Rating= 9 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },                
                new UserBook
                {
                    Id = new Guid("def9c864-7423-49f6-8ff6-5b96096c7381"),
                    Book = new Book {Id = Guid.NewGuid(), Title = "Solaris", Author = new Author {Id = Guid.NewGuid(), FirstName="Stanisław",
                            LastName = "Lem"}, Genre = new Genre {Id = Guid.NewGuid(), Name = "Sci-fi"}, Rating= 9 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                },               
                new UserBook
                {
                    Id = new Guid("def9c864-7423-49f6-8ff6-5b96096c7344"),
                    Book = new Book {Id = Guid.NewGuid(), Title = "The Long Earth", Author = new Author {Id = Guid.NewGuid(), FirstName="Terry",
                            LastName = "Pratchett"}, Genre = new Genre {Id = Guid.NewGuid(), Name = "Fantasy"}, Rating= 8 },
                    User = alonzo,
                    Borrowed = false,
                    IsPublic = true
                }
            };
        }

        public IEnumerable GetAll()
        {
            return UserBooks;
        }

        public UserBook GetById(Guid id)
        {
            return UserBooks.Where(a => a.Id == id).FirstOrDefault();
             //co ta funkcja robi int?? 
        }

        public void Add(UserBook t)
        {
            throw new NotImplementedException();
        }

        public UserBook Update(UserBook t)
        {
            // var userbook = 
            // from ub in UserBooks
            //     where t.Id == ub.Id
            //     select ub;

            UserBook before = GetById(t.Id);
            before.Borrowed = true;
            int index = UserBooks.IndexOf(before);
            UserBooks[index] = before;
            return before;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            return 0;
        }
    }
}
