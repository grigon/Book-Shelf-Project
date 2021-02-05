using System;
using System.Collections;
using bookshelf.Context;
using bookshelf.DAL;
using bookshelf.Model.Books;

namespace bookshelf.FakeData
{
    public class DataFakeRepositoty : IBaseRepository<UserBook>
    {
        private readonly IBaseContext _context;
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