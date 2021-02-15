using System;
using System.Collections;
using bookshelf.Context;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly BaseDBContext _context;

        public BookRepository(BaseDBContext context)
        {
            _context = context;
        }

        public BookRepository()
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetAll()
        {
            throw new NotImplementedException();
        }

        public Book GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Book Add(Book t)
        {
            throw new NotImplementedException();
        }

        public Book Update(Book t)
        {
            throw new NotImplementedException();
        }

        public Book Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public UserBook UpdateIsPublic(Book t)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }
    }
}