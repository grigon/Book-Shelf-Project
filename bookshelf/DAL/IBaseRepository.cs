using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Model.Books;
using bookshelf.Model.Users;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T> where T : class

    {
        Task<T[]> GetAll();
        Task<T> GetById(Guid id);
        void Add(T t);
        Task<T> Update(T t);
        void Remove(T entity);
        Task<bool> Commit();
    }
}