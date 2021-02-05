using System;
using System.Collections;
using bookshelf.Context;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public class UserBookRepository : IBaseRepository<UserBook>
    {
        private readonly IBaseContext _context;

        public UserBookRepository(IBaseContext context)
        {
            _context = context;
        }

        public IEnumerable GetAll()
        {
            throw new NotImplementedException();
        }

        public UserBook GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(UserBook t)
        {
            throw new NotImplementedException();
        }

        public UserBook Update(UserBook t)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }
    }
}