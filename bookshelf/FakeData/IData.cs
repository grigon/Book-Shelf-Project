using bookshelf.Model.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.FakeData
{
    public interface IData
    {
        List<Book> GetAllBooks();
        
    }
}
