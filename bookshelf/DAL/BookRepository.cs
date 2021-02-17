using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly BaseDbContext _context;

        public BookRepository(BaseDbContext context)
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

        Task<Book> IBaseRepository<Book>.GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<Book>.Add(Book t)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Update(Book t)
        {
            throw new NotImplementedException();
        }
        

        Task<Book[]> IBaseRepository<Book>.GetAll()
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

      

        public void Remove(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        Task<bool> IBaseRepository<Book>.Commit()
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