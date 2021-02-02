using System.Collections.Generic;
using bookshelf.Context;
using bookshelf.Entities;

namespace bookshelf.DAL
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly BaseDBContext _context;
        
        public BookRepository()
        {
            _context = new BaseDBContext();
        }
        public BookRepository(BaseDBContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Book> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Book GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Book t)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Book t)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}