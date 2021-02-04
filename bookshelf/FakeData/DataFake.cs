using bookshelf.Model.Books;
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
        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public void CreateBooksFake()
        {
            Books = new List<Book>()
            {
                //new Book {Id = new Guid(), Title = "Marcin", new Author {Id = new Guid(), FirstName="Marek",
                //LastName = "Marcel"}, new Genre {Id = new Guid(), Name = "Thriler"}, Rating= 15 } 
                //    new Author {new Guid (), "Maricin1" , "Nazwisko2" },
                //new Genre {new Guid(), "Triller" }, 15 }
            };
        }
        //public DataFake()
        //{
        //    Book book = new Book();
        //    book.
        //}
        
        //{
        //     new Guid(), "Title", new Author {new Guid (), "Maricin1" , "Nazwisko2" },
        //        new Genre {new Guid(), "Triller" }, 15 
        //}
    }
}
