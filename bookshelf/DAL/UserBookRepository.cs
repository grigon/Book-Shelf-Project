using System;
using System.Collections;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public class UserBookRepository : IBaseRepository<UserBook>
    {
        public IEnumerable GetAll()
        {
            throw new System.NotImplementedException();
        }

        public UserBook GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(UserBook t)
        {
            throw new System.NotImplementedException();
        }

        public UserBook Update(UserBook t)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public UserBook GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}