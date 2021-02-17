using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public class UserBookRepository : IBaseRepository<UserBook>
    {
        private readonly BaseDbContext _context;

        public UserBookRepository(BaseDbContext context)
        {
            _context = context;
        }

        public IEnumerable GetAll()
        {
            var result = _context.UserBooks;
            return result;
        }

        Task<UserBook> IBaseRepository<UserBook>.GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<UserBook>.Add(UserBook t)
        {
            throw new NotImplementedException();
        }

        public Task<UserBook> Update(UserBook t)
        {
            throw new NotImplementedException();
        }
        

        Task<UserBook[]> IBaseRepository<UserBook>.GetAll()
        {
            throw new NotImplementedException();
        }

        public UserBook GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserBook Add(UserBook t)
        {
            throw new NotImplementedException();
        }
        

        public void Remove(UserBook entity)
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        Task<bool> IBaseRepository<UserBook>.Commit()
        {
            throw new NotImplementedException();
        }

        public UserBook Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public UserBook UpdateIsPublic(UserBook t)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }
    }
}